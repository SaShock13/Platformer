using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowDelete : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        StartCoroutine(DeleteObject());
    }

    

    IEnumerator DeleteObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
