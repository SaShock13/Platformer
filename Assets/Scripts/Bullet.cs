using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Bullet triggerred Something");
        if (collider.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<EnemyActions>().TakeDamage();
            StartCoroutine(nameof(BulletInTarget));
        }

    }

    IEnumerator BulletInTarget()
    {
        yield return new WaitForSeconds(0.05f);        
        Destroy(gameObject);
    }
}
