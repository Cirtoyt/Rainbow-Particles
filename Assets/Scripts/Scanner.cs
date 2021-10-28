using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Transform scanner;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private int particlesPerScan = 100;
    //[SerializeField] private float randonDistributionScale = 100;
    [SerializeField] [Range(0.1f, 180)] private float scannerFOVAngle = 60;
    [SerializeField] private float scannerSpawnDelay = 0.1f;

    private bool isScanning = false;
    //private float randomDistributionOffset = 0;
    private float scannerSpawnDelayTimer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (isScanning)
        {
            scannerSpawnDelayTimer += Time.deltaTime;
            if (scannerSpawnDelayTimer >= scannerSpawnDelay)
            {
                scannerSpawnDelayTimer = 0;
                SpawnParticles();
            }
        }
    }

    private void SpawnParticles()
    {
        for (int x = 0; x < particlesPerScan; x++)
        {
            // Old code theory
            // Get perlin noise x/y coords
            //float xCoord = x / particlesPerScan;
            //float yCoord = randomDistributionOffset / randonDistributionScale;
            //randomDistributionOffset += randomDistributionOffset / (randonDistributionScale * 2);

            // Get raycast direction & distance from the centre
            Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            float fromCentreDist = Random.Range(0.0f, 1.0f); //Mathf.PerlinNoise(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            //float coneMultiplier = scannerFOVAngle / 180;

            // Translate raycast direction into forward, right, and up vectors that are scaled by the scanner FOV range cone multiplier
            //Vector3 raycastForwardDir = scanner.forward * (1 - coneMultiplier);
            //Vector3 raycastRightDir = scanner.right * randomDir.x * centreDist * coneMultiplier;
            //Vector3 raycastUpDir = scanner.up * randomDir.y * centreDist * coneMultiplier;
            //Vector3 raycastDir = raycastForwardDir + raycastRightDir + raycastUpDir;
            // Create raycast direction from crossproduct between raycast right/up direction and scanner forward direction,
            // then rotating along cross product by random 'from centre distance', scaled by scanner FOV angle
            Vector3 raycastRightUpDir = (scanner.right * randomDir.x) + (scanner.up * randomDir.y);
            Vector3 raycastCrossProduct = Vector3.Cross(scanner.forward, raycastRightUpDir);
            Vector3 raycastDir = Quaternion.AngleAxis((scannerFOVAngle / 2) * fromCentreDist, raycastCrossProduct) * scanner.forward;

            // Cast ray and spawn particle at hit point
            Physics.Raycast(scanner.position, raycastDir, out RaycastHit hitInfo);
            Instantiate(particlePrefab, hitInfo.point, Quaternion.identity);
        }
    }

    private void OnScan(InputValue value)
    {
        if (value.Get<float>() == 1)
        {
            scannerSpawnDelayTimer = scannerSpawnDelay;
            isScanning = true;
        }
        else
        {
            isScanning = false;
        }
    }
}
