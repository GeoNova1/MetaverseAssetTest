using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsHandler : MonoBehaviour
{
    [SerializeField] GameObject outOfBoundsUI;
    [SerializeField] Transform player;
    [SerializeField] float distance;
    [SerializeField] float time;
    [SerializeField] string loseReason = "Went out of bounds";

    IEnumerator outOfBoundsEnumerator;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > distance)
        {
            if (outOfBoundsEnumerator == null)
            {
                outOfBoundsEnumerator = OutOfBounds();
                StartCoroutine(outOfBoundsEnumerator);
            }
        }
        else if (outOfBoundsEnumerator != null)
        {
            StopCoroutine(outOfBoundsEnumerator);
            outOfBoundsEnumerator = null;

            outOfBoundsUI.SetActive(false);
        }
    }

    IEnumerator OutOfBounds()
    {
        outOfBoundsUI.SetActive(true);
        yield return new WaitForSeconds(time);

        WinLoseUI.Instance.Lose(loseReason);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distance);        
    }
}
