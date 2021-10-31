using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float walkSpeedMultiplier = 1;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private float cameraSpeedMultiplier = 1;
    [SerializeField] [Range(0, 89.99f)] private float topCameraMaxAngle = 90f;
    [SerializeField] [Range(0, 89.99f)] private float bottomCameraMaxAngle = 90f;
    [Header("References")]
    [SerializeField] private ParticleManager particleMngr;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector2 lookDirection;
    private float pitchLookRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        RotateCamera();
        MovePlayer();
    }

    private void RotateCamera()
    {
        float pitchDelta = -lookDirection.y * cameraSpeedMultiplier * Time.deltaTime;
        float yawDelta = lookDirection.x * cameraSpeedMultiplier * Time.deltaTime;

        pitchLookRotation += pitchDelta;
        pitchLookRotation = Mathf.Clamp(pitchLookRotation, -topCameraMaxAngle, bottomCameraMaxAngle);

        fpsCamera.transform.localRotation = Quaternion.Euler(Vector3.right * pitchLookRotation);
        transform.Rotate(Vector3.up * yawDelta);
    }

    private void MovePlayer()
    {
        Vector3 cameraForward = fpsCamera.transform.forward;
        cameraForward.y = 0;
        Vector3 cameraRight = fpsCamera.transform.right;

        Vector3 velocity = ((cameraForward * moveDirection.z) + (cameraRight * moveDirection.x)).normalized * walkSpeedMultiplier;
        rb.velocity = velocity;

        if (velocity != Vector3.zero)
        {
            particleMngr.OnMove();
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 vec2Val = value.Get<Vector2>();
        moveDirection = new Vector3(vec2Val.x, 0, vec2Val.y);
    }

    private void OnLook(InputValue value)
    {
        lookDirection = value.Get<Vector2>();
    }
}
