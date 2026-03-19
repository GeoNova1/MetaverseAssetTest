using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPathFollower : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] CinemachinePathBase path;
    [SerializeField] float speed;
    
    float pos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (path == null) return;

        pos += speed * Time.fixedDeltaTime;

        Vector3 targetPosition = path.EvaluatePositionAtUnit(pos, CinemachinePathBase.PositionUnits.Distance);
        Quaternion targetRotation = path.EvaluateOrientationAtUnit(pos, CinemachinePathBase.PositionUnits.Distance);

        rb.MovePosition(targetPosition);
        rb.MoveRotation(targetRotation);
    }
}
