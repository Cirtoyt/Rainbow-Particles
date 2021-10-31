using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColourUpdater : MonoBehaviour
{
    [SerializeField] private float particleColourStartDepth;
    [SerializeField] private float particleColourEndDepth;
    [SerializeField] private Gradient colourGradient;
    [SerializeField] private bool updateColourPerFrame = false;

    private Renderer renderer;
    private Transform player;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set first colour based on initial distance
        UpdateColour();
    }

    void Update()
    {
        if (updateColourPerFrame) UpdateColour();
    }

    private void UpdateColour()
    {
        // get distance from player
        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceFromPlayer >= particleColourStartDepth && distanceFromPlayer <= particleColourEndDepth)
        {
            // get scale value based on position between colour start depth and end depth
            float colourPercIdx = (distanceFromPlayer - particleColourStartDepth) / (particleColourEndDepth - particleColourStartDepth);
            Debug.Log(colourPercIdx);
            // index colour from pre-set gradient (maybe serializable field)
            // set collider's renderer's material colour
            renderer.material.color = colourGradient.Evaluate(colourPercIdx);
        }
        else if (distanceFromPlayer < particleColourStartDepth)
        {
            // index colour from pre-set gradient (maybe serializable field)
            // set collider's renderer's material colour
            renderer.material.color = colourGradient.Evaluate(0);
        }
        else
        {
            renderer.material.color = colourGradient.Evaluate(1);
        }
    }
}
