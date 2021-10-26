using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePivot : MonoBehaviour
{
    private Transform fpsCameraTrans;

    void Start()
    {
        fpsCameraTrans = Camera.main.transform;
    }

    void LateUpdate()
    {
        //transform.rotation = Quaternion.Euler(0, fpsCameraTrans.rotation.eulerAngles.y, 0);
        transform.LookAt(fpsCameraTrans);
    }
}
