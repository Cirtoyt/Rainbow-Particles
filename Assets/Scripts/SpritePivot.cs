using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePivot : MonoBehaviour
{
    [SerializeField] private bool inverseDirection = false;
    private Transform fpsCameraTrans;

    void Start()
    {
        fpsCameraTrans = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (inverseDirection)
        {
            transform.LookAt(transform.position + (transform.position - fpsCameraTrans.position));
        }
        else
        {
            transform.LookAt(fpsCameraTrans);
        }
    }
}
