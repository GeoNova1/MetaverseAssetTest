using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector2 moveInput;

    [SerializeField] Transform motorTrans;

    [SerializeField] float maxMotorAngle;
    [SerializeField] float motorRotationSpeed;

    [SerializeField] float motorForwardForce;
    [SerializeField] float motorReverseForce;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // Turn outboard motor
        Quaternion targetRot = Quaternion.identity;
        if (moveInput.x != 0f)
        {
            targetRot = moveInput.x > 0f ? Quaternion.Euler(0f, -maxMotorAngle, 0f)
                                                    : Quaternion.Euler(0f, maxMotorAngle, 0f);
        }
        motorTrans.localRotation = Quaternion.RotateTowards(motorTrans.localRotation, targetRot, motorRotationSpeed * Time.deltaTime);

        // Apply outboard motor force
        if (moveInput.y != 0f)
        {
            float forceMultiplier = moveInput.y > 0f ? motorForwardForce : -motorReverseForce;
            Vector3 force = motorTrans.forward * forceMultiplier;
            rb.AddForceAtPosition(force, motorTrans.position);
        }
    }
}
