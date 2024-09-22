using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    [Header("PointEffector2D взрыва")]
    [SerializeField] PointEffector2D explosion;
    AudioSource sound;
    [SerializeField] GameObject anotherTrigger;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            explosion.enabled = true;
            sound.Play();
            StartCoroutine(nameof(EffectorOff));
        }
    }

    IEnumerator EffectorOff()
    {
        yield return new WaitForSeconds(0.7f);
        explosion.enabled = false;
        Destroy(anotherTrigger);
        Destroy(gameObject);
    }
}
