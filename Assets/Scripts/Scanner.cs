using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Transform scanner;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private float scannerSpawnDelay = 0.1f;

    private bool isScanning = false;
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
                Physics.Raycast(scanner.position, scanner.forward, out RaycastHit hitInfo);
                Instantiate(particlePrefab, hitInfo.point, Quaternion.identity);
            }
        }
    }

    private void OnScan(InputValue value)
    {
        if (value.Get<float>() == 1)
        {
            scannerSpawnDelayTimer = 0;
            isScanning = true;
        }
        else
        {
            isScanning = false;
        }
    }
}
