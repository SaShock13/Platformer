using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LifeUp : MonoBehaviour
{
    AudioSource bonusSound;
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;
    bool isTaken = false;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bonusSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isTaken)
            {
                isTaken = true;
                collision.GetComponent<LivesCounter>().IncreseLifes();
                spriteRenderer.enabled = false;
                if (particles)
                {
                    particles.Play();
                }
                if (bonusSound)
                {
                    bonusSound.Play();
                }
                StartCoroutine(PausedDestroy(0.6f));
            }
        }
    }
    IEnumerator PausedDestroy(float pauseTimeInSeconds)
    {
        yield return new WaitForSeconds(pauseTimeInSeconds);
        Destroy(gameObject);
    }
}
