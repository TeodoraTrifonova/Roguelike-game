using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;
    private bool isFlipped = false;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int points = 10;

    [SerializeField]
    private GameObject walkingParticles;

    [SerializeField]
    private GameObject feetPos;

    private float particleTimer;

    [SerializeField]
    private GameObject deathParticles;

    [SerializeField]
    private int attackDamage = 20;

    [SerializeField]
    private Vector3 attackOffset;

    [SerializeField]
    private float attackRange = 1f;

    public float AttackRange { get => attackRange; }

    [SerializeField]
    private LayerMask attackMask;

    [SerializeField]
    private bool ShowAttackRangeDebug;

    void Start()
    {
        currentHealth = maxHealth;
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

        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Debug.Log("I'm attacking!" + pos);
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().UpdateHealth((-attackDamage) / 4); // poradi nqkakwa prichina avera udrq chetiri puti s edin attack i towa beshe nai lesniq fix
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
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        //Hit animation
        if (currentHealth <= 0)
        {
            Die();

        }

    }

    void Die()
    {
        //animator.SetTrigger("isDead");
        ScoreCounter.score += points;

        GetComponent<Collider2D>().enabled = false;

        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (ShowAttackRangeDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x * attackOffset.x, transform.position.y * attackOffset.y), attackRange);
        }
    }
}
