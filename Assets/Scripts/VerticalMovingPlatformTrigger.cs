using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatformTrigger : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            rb.isKinematic = false;
        }
    }
}
