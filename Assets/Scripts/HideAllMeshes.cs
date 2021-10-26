using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAllMeshes : MonoBehaviour
{
    void Start()
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}
