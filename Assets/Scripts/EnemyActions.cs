using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    EnemyLifes enemyLifes;

    [Header("Объект бонуса, выпадающего из трупа")]
    [SerializeField] GameObject bonus;

    [Header("Объект взрыва")]
    [SerializeField] GameObject explode;

    [Header("Вспомогательные объекты")]
    [SerializeField] GameObject leftTrigger;
    [SerializeField] GameObject rightTrigger;

    [Header("Скорость ходьбы врага")]
    [SerializeField] float speed;

    [Header("Сколько курит враг перед разворотом")]
    [SerializeField] float smokingTime;

    [Header("Если отмечено, то враг взрывается после смерти")]
    [SerializeField] bool isExplosive;

    bool walkLeft = true;
    [Header("Если отмечено, то враг неподвижен")]
    [SerializeField] bool isStoped ;
    Vector3 directionVector;



    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        enemyLifes = GetComponent<EnemyLifes>();
    }

    private void Update()
    {
        Walk();
    }

    /// <summary>
    /// Метод получения урона
    /// </summary>
    public void TakeDamage()
    {
        StartCoroutine(nameof(Damaged));
    }

    /// <summary>
    /// Корутина смерти врага и его удаление
    /// </summary>
    /// <returns></returns>
    IEnumerator Death()
    {
        
        gameObject.GetComponentInParent<Collider2D>().enabled = false;
        isStoped = true;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        if (isExplosive)
        {
            GameObject explosion = Instantiate(explode, transform.position, Quaternion.identity);
            explosion.GetComponent<PointEffector2D>().enabled = true;
            explosion.GetComponent <AudioSource>().Play();
            yield return new WaitForSeconds(0.5f);
            Destroy(explosion);
        }

        GameObject oneBonus = Instantiate(bonus, transform.position, Quaternion.identity);
        oneBonus.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Destroy(gameObject);
    }

    /// <summary>
    /// Логика раненния врага и проверка на смерть
    /// </summary>
    /// <returns></returns>
    IEnumerator Damaged()
    {
        yield return new WaitForSeconds(0.15f);
        enemyLifes.DecreseLifes();
        if (!enemyLifes.isALive)
        {
            StartCoroutine(nameof(Death));
        }else animator.SetTrigger("Damage");
    }

    /// <summary>
    /// Метод ходьбы
    /// </summary>
    void Walk()
    {
        if (!isStoped)
        {
            animator.SetBool("isWalking", true);
            directionVector = walkLeft?Vector3.left:Vector3.right;
            transform.position += directionVector * speed * Time.deltaTime;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.collider.CompareTag("DamageZone") | collision.collider.CompareTag("DeadZone"))
        {
            StartCoroutine(nameof(Death));
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger");

        if (collider.CompareTag("DamageZone"))
        {
            StartCoroutine(nameof(Death));
        }
        if (collider.name == "leftTrigger"| collider.name == "rightTrigger")
        {
            StartCoroutine(nameof(Smoking));
        }
    }

    /// <summary>
    /// Логика остановки врага (На перекур) и разворота в другую сторону
    /// </summary>
    /// <returns></returns>
    IEnumerator Smoking()
    {
        isStoped = true;
        yield return new WaitForSeconds(smokingTime);
        isStoped = false;
        walkLeft = !walkLeft;
        sr.flipX = !sr.flipX;
    }
}
