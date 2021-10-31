using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColourUpdater : MonoBehaviour
{
    [SerializeField] private float particleColourStartDepth = 3;
    [SerializeField] private float particleColourEndDepth = 8;
    [SerializeField] private Gradient colourGradient;
    [SerializeField] private bool updateColourPerFrame = false;
    [SerializeField] private float colourChangeSmoothing = 1;

    private Renderer spriteRenderer;
    private Transform player;
    private float targetColourPercIdx;
    private float currentColourPercIdx;

    void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set first colour based on initial distance
        UpdateTargetColourPercIdx();
        currentColourPercIdx = targetColourPercIdx;
        UpdateColour(currentColourPercIdx);
    }

    void Update()
    {
        if (updateColourPerFrame) UpdateColour(targetColourPercIdx);

        if (currentColourPercIdx != targetColourPercIdx)
        {
            //Debug.Log("I'm updating!");
            // Smooth towards target colour over time
            if (currentColourPercIdx < targetColourPercIdx)
            {
                currentColourPercIdx += colourChangeSmoothing * Time.deltaTime;
                if (currentColourPercIdx > targetColourPercIdx) currentColourPercIdx = targetColourPercIdx;
            }
            else
            {
                currentColourPercIdx -= colourChangeSmoothing * Time.deltaTime;
                if (currentColourPercIdx < targetColourPercIdx) currentColourPercIdx = targetColourPercIdx;
            }
            //UpdateColour(currentColourPercIdx); or:
            UpdateParticle();
        }
    }

    private void UpdateTargetColourPercIdx()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);

        // Closer than start depth
        float colourPercIdx = 0;
        // In middle of start and end depth
        if (distanceFromPlayer >= particleColourStartDepth && distanceFromPlayer <= particleColourEndDepth)
        {
            colourPercIdx = (distanceFromPlayer - particleColourStartDepth) / (particleColourEndDepth - particleColourStartDepth);
        }
        // Further than end depth
        else if (distanceFromPlayer > particleColourEndDepth)
        {
            colourPercIdx = 1;
        }

        targetColourPercIdx = colourPercIdx;
    }

    private void UpdateColour(float percIdx)
    {
        spriteRenderer.material.color = colourGradient.Evaluate(percIdx);
    }

    public void UpdateParticle()
    {
        UpdateTargetColourPercIdx();
        UpdateColour(currentColourPercIdx);
    }
}
