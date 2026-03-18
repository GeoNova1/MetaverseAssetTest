using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockTrigger : MonoBehaviour
{
    [SerializeField] DockUI dockUI;

    [Header("Docking requirements")]
    [SerializeField] float maxAngle; // The angle between the dock & players forward vectors
    [SerializeField] float maxSpeed;
    [SerializeField] float dockTime;

    int collidersInside;
    IEnumerator dockEnumerator;
    float timeSpentDocking;


    void OnTriggerEnter(Collider col)
    {
        if (!col.attachedRigidbody.CompareTag("Player")) return;
        
        collidersInside++;
    }
    void OnTriggerExit(Collider col)
    {
        if (!col.attachedRigidbody.CompareTag("Player")) return;

        collidersInside--;

        // Stop docking if the player has left the area
        if (collidersInside == 0)
            TryStopDocking();
    }

    void OnTriggerStay(Collider col)
    {
        var attachedRigidbody = col.attachedRigidbody;
        if (!attachedRigidbody.CompareTag("Player")) return;

        float angle = Vector3.Angle(transform.forward, attachedRigidbody.transform.forward);
        float speed = attachedRigidbody.velocity.magnitude;

        // Dock if the player is angled correctly & is slow
        if (angle < maxAngle && speed < maxSpeed)
            TryStartDocking();
        else
            TryStopDocking();
    }


    void TryStartDocking()
    {
        if (dockEnumerator != null) return;

        dockEnumerator = Dock();
        StartCoroutine(dockEnumerator);
    }
    void TryStopDocking()
    {
        if (dockEnumerator == null) return;

        StopCoroutine(dockEnumerator);
        dockEnumerator = null;

        timeSpentDocking = 0f;
        dockUI.SetActive(false);
    }

    IEnumerator Dock()
    {
        print("Started Docking");
        timeSpentDocking = 0f;
        dockUI.SetDockingProgress(0f);
        dockUI.SetActive(true);

        while (timeSpentDocking < dockTime)
        {
            timeSpentDocking += Time.deltaTime;
            dockUI.SetDockingProgress(timeSpentDocking / dockTime);
            
            yield return null;
        }
        
        print("Finished Docking");
    }
}
