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
    [SerializeField] [Range(0.1f, 180)] private float coneAngleRange = 60;
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
            // Get perlin noise x/y coords
            //float xCoord = x / particlesPerScan;
            //float yCoord = randomDistributionOffset / randonDistributionScale;
            //randomDistributionOffset += randomDistributionOffset / (randonDistributionScale * 2);

            float coneMultiplier = coneAngleRange / 180;
            float xDir = Random.Range(-1f, 1f);
            float yDir = Random.Range(-1f, 1f);
            Vector2 randomDir = new Vector2(xDir, yDir).normalized;
            float centreDist = Mathf.PerlinNoise(Random.Range(0.001f, 0.999f), Random.Range(0.001f, 0.999f));

            Physics.Raycast(scanner.position, scanner.forward + (scanner.right * randomDir.x * centreDist * coneMultiplier) + (scanner.up * randomDir.y * centreDist * coneMultiplier), out RaycastHit hitInfo);
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
