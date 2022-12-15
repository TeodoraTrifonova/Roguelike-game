using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Enemy enemy;

    private int currentHealth;

    private bool isFlipped = false;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int stackCounter;

    
    public bool isInvulnerable = false;


    //public HealthBarController healthBar;


    void Start()
    {
        enemy = GetComponent<Enemy>();
        currentHealth = enemy.MaxHealth;
        stackCounter = 0;
       // healthBar.SetMaxHealth(currentHealth);
  
    }



    public void BossAttack()
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

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            transform.rotation = new Quaternion(0f, 0, 0f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            isFlipped = true;
        }

    }
    private void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        animator.SetTrigger("BossHurt");
        animator.SetInteger("BossStacks", stackCounter++);

        if(stackCounter == 3)
        {
            animator.SetBool("Mage Rage", true);
        }

       // healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsBossDead",true);
        ;
        ScoreCounter.instance.IncrementScore(enemy.Points);
       
    }

    public void OnDeathAnimationFinished()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().Damage);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvulnerable && collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().UpdateHealth(-1 * enemy.SpellDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isInvulnerable && collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().UpdateHealth(-1 * enemy.SpellDamage);
        }
    }
}
