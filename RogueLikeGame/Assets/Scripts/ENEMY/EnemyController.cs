using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Enemy enemy;

    private int currentHealth;

    private bool isFlipped = false;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject walkingParticles;

    [SerializeField]
    private GameObject feetPos;

    private float particleTimer;

    [SerializeField]
    private GameObject deathParticles;



    void Start()
    {
        enemy = GetComponent<Enemy>();
        currentHealth = 100;
    }

    public void SpawnParticles()
    {
        if (particleTimer < 0.1f)
        {
            particleTimer += Time.deltaTime;
        }
        else
        {
            Instantiate(walkingParticles, feetPos.transform.position, transform.rotation);
            particleTimer = 0f;
        }
    }


    public void Attack()
    {
        LookAtPlayer();

        Vector3 attackPosition = transform.position;
        attackPosition += transform.right * enemy.AttackOffset.x;
        attackPosition += transform.up * enemy.AttackOffset.y;

        Collider2D colliderInfo = Physics2D.OverlapCircle(attackPosition, enemy.AttackRange, enemy.AttackMask);
        if (colliderInfo != null)
        {
            colliderInfo.GetComponent<PlayerHealth>().UpdateHealth(-1 * enemy.AttackDamage / 4); // poradi nqkakwa prichina avera udrq chetiri puti s edin attack i towa beshe nai lesniq fix
        }
    }
    public void LookAtPlayer()
    {

        if (transform.position.x < player.transform.position.x && isFlipped )
        {
            transform.rotation = new Quaternion(0f, 0, 0f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.transform.position.x && !isFlipped)
        {
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            isFlipped = true;
        }

    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
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

        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().damage);
        }
    }
}
