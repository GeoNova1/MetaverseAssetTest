using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPathFollower : MonoBehaviour
{
    //CinemachinePathBase path;
    Rigidbody rb;

    [SerializeField] CinemachinePathBase path;

    float pos;
    [SerializeField] float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
