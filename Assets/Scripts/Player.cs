using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    [Header("Скорость бега игрока")]
    [SerializeField] float playerRunSpeed;

    [Header("Сила прыжка игрока")]
    [SerializeField] float jumpForce;

    [Header("Velocity опасного объекта, достаточный для смерти")]
    [SerializeField] float deadVelocity;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    bool isInvincible = false;
    LivesCounter livesCounter;
    SpriteRenderer sprite;
    GameObject player;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float horizontal;
    bool isOnTheFloor;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = transform.parent.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        livesCounter = GetComponent<LivesCounter>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        KeyListener();
    }

    void KeyListener()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //??? Как лучше предотвращать двойной прыжок????
            if (isOnTheFloor)
            {
                Jump();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(nameof(Death));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerRunSpeed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerRunSpeed /= 2;
        }

        horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            Move(new Vector3(horizontal, 0, 0));
        }
        else animator.SetBool("isRunning", false);
    }

    /// <summary>
    /// Передвигает игрока и воспроизводит анимацию бега
    /// </summary>
    /// <param name="vector3"></param>
    void Move(Vector3 vector3)
    {
        animator.SetBool("isRunning", true);
        sr.flipX = horizontal < 0 ? true : false;
        transform.position += vector3 * playerRunSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Задает силу для прыжка и включает анимацию
    /// </summary>
    void Jump()
    {
        isOnTheFloor = false;
        animator.SetTrigger("Jump");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor")) // Проверка на соприкосновение с полом
        {
            isOnTheFloor = true;
        }
        if (collision.collider.CompareTag("Danger"))
        {
            if (collision.rigidbody.velocity.magnitude > deadVelocity)
            {
                StartCoroutine(nameof(Death));
            }
        }
        if (collision.collider.CompareTag("DeadZone"))
        {
            StartCoroutine(nameof(Death));
        }
        if (collision.collider.CompareTag("DamageZone"))
        {
            TakeADamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("DamageZone"))
        {
            TakeADamage();
        }
        if (collider.CompareTag("DeadZone"))
        {
            StartCoroutine(nameof(Death));
        }

        if (collider.CompareTag("WinZone"))
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }

    void TakeADamage()
    {

        if (!isInvincible)
        {
            //livesCounter.DecreseLifes();
            Debug.Log("OldController");
            if (!livesCounter.isALive)
            {
                StartCoroutine(nameof(Death));
            }
            else StartCoroutine(nameof(TemporaryInvincibility)); 
        }
    }

    IEnumerator TemporaryInvincibility()
    {
        isInvincible = true;
        for (int i = 0; i < 20; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        sprite.enabled = true;
        isInvincible = false;
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        gameOverPanel.SetActive(true);
        GameObject.Destroy(gameObject);
    }
}
