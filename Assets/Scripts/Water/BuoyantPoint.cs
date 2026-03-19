using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyantPoint : MonoBehaviour
{
    [SerializeField] float mass;
    [SerializeField] float height;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (rb == null)
            gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        float weight = mass * Physics.gravity.y;

        float waterHeight = WaterHeight.Instance.GetWaterHeightAtPosition(transform.position);
        float amountSubmerged = Mathf.Clamp(waterHeight - transform.position.y, 0f, height);

        float percentSubmerged = amountSubmerged / height;

        float force = -weight * percentSubmerged;


        rb.AddForceAtPosition(Vector3.up * force, transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, Vector3.up * height + transform.position);
    }
}