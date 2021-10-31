using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private LayerMask particleLayer;
    [SerializeField] private float particleColourStartDepth;
    [SerializeField] private float particleColourEndDepth;
    [SerializeField] private Gradient colourGradient;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnMove()
    {
        Collider[] pColliders = Physics.OverlapSphere(player.position, particleColourEndDepth, particleLayer);

        foreach (Collider pCollider in pColliders)
        {
            // get distance from player
            float distanceFromPlayer = Vector3.Distance(pCollider.transform.position, player.position);
            if (distanceFromPlayer >= particleColourStartDepth)
            {
                // get scale value based on position between colour start depth and end depth
                float colourPercIdx = (distanceFromPlayer - particleColourStartDepth) / (particleColourEndDepth - particleColourStartDepth);
                Debug.Log(colourPercIdx);
                // index colour from pre-set gradient (maybe serializable field)
                // set collider's renderer's material colour
                pCollider.GetComponent<Renderer>().material.color = colourGradient.Evaluate(colourPercIdx);
            }
            else
            {
                // index colour from pre-set gradient (maybe serializable field)
                // set collider's renderer's material colour
                pCollider.GetComponent<Renderer>().material.color = colourGradient.Evaluate(0);
            }
        }
    }
}
