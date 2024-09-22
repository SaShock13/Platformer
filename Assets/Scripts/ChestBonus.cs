using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBonus : MonoBehaviour
{
    [SerializeField] int bonusValue;
    AudioSource bonusSound;
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;
    bool isOpened = false;
    bool isInTrigger = false;
    Collider2D playerCollider;
    Animator animator;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bonusSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E)&&!isOpened) 
            {
                OpenChest();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = true;
            playerCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
                isInTrigger = false;
        }
    }

    void OpenChest()
    {
        animator.SetBool("Opened", true);
        isOpened = true;
    }

    void OnChestOpened()
    {
        playerCollider.GetComponent<BonusCounter>().TakeABonus(bonusValue,true);
        if (particles)
        {
            particles.Play();
        }
        if (bonusSound)
        {
            bonusSound.Play();
        }
    }

}
