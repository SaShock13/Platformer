using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallTrigger : MonoBehaviour
{
    [Header("Rigidbody ловушки")]
    [SerializeField] Rigidbody2D trapRB;

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            trapRB.isKinematic = false;
        }
    }
}

