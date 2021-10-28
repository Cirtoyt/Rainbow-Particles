using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private float particleColourStartDepth;
    [SerializeField] private float particleColourEndDepth;
    [SerializeField] private LayerMask particleLayer;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Collider[] pColliders = Physics.OverlapSphere(player.position, particleColourEndDepth, particleLayer);

        foreach (Collider pCollider in pColliders)
        {
            // get distance from player
            // get scale value based on position between colour start depth and end depth
            // index colour from pre-set gradient (maybe serializable field)
            // set collider's renderer's material colour
        }
    }
}
