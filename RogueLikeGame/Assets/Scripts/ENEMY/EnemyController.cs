using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Enemy enemy;

    private Rigidbody2D rb;

    private int currentHealth;

    private Transform player;

    [SerializeField]
    private GameObject walkingParticles;

    [SerializeField]
    private GameObject feetPos;

    private float particleTimer;

    [SerializeField]
    private GameObject deathParticles;

    public HealthBarController healthBar;


    void Start()
    {
        enemy = GetComponent<Enemy>();
        currentHealth = enemy.MaxHealth;
        healthBar.SetMaxHealth(currentHealth);
        SpawnParticles();
        rb = GetComponent<Rigidbody2D>();
    }

    void SpawnParticles()
    {
        Instantiate(deathParticles, feetPos.transform.position, transform.rotation, GameObject.Find("Particles").transform);
    }

    public void WalkingParticles()
    {
        if (particleTimer < 0.1f)
        {
            particleTimer += Time.deltaTime;
        }
        else
        {
            Instantiate(walkingParticles, feetPos.transform.position, transform.rotation, GameObject.Find("Particles").transform);
            particleTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!player.IsDestroyed() && player != null)
        {
            rb.velocity = new Vector2(0, 0); // removes velocity from the enemy


            if (Vector2.Distance(player.transform.position, rb.position) <= enemy.AttackRange)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y);

                Vector2 newPos = Vector2.MoveTowards(rb.transform.position, target, enemy.MoveSpeed * Time.fixedDeltaTime);

                Vector2 direction = new Vector2(newPos.x - target.x, newPos.y - target.y).normalized * -1;

                animator.SetFloat("moveX", direction.x);
                animator.SetFloat("moveY", direction.y);

                rb.MovePosition(newPos);

                WalkingParticles();
            }
        }
    }

    public void Attack()
    {

        Vector3 attackPosition = transform.position;
        attackPosition += transform.right * enemy.AttackOffset.x;
        attackPosition += transform.up * enemy.AttackOffset.y;

        Collider2D colliderInfo = Physics2D.OverlapCircle(attackPosition, enemy.AttackRange, enemy.AttackMask);
        if (colliderInfo != null)
        {
            colliderInfo.GetComponent<PlayerHealth>().UpdateHealth(-1 * enemy.AttackDamage / 4); // poradi nqkakwa prichina avera udrq chetiri puti s edin attack i towa beshe nai lesniq fix
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("isDead");
        ScoreCounter.instance.IncrementScore(enemy.Points);

        GetComponent<Collider2D>().enabled = false;

        SpawnParticles();

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().Damage);
        }
    }
}
