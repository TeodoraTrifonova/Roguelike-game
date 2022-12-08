using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    private bool isFlipped=false;
    public Transform player;
    [SerializeField]
    private GameObject walkingParticles;

    [SerializeField]
    private GameObject feetPos;

    private float particleTimer;

    [SerializeField]
    private GameObject deathParticles;

    public int attackDamage = 20;
    public Vector3 attackOffset;
    public float attackRange = .1f;
    public LayerMask attackMask;
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
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Debug.Log("I'm attacking!" + pos);
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if(colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
        }
    }
    public void LookAtPlayer()
    {
        SpawnParticles();
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;  
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }
    }
   public void TakeDamage(int damage)
    {
        Debug.Log("YOURE HERE");
        currentHealth -= damage;
        //Hit animation
        if(currentHealth <= 0)
        {
            Die();
           
        }
       
    }

   void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetTrigger("isDead");

        GetComponent<Collider2D>().enabled = false;
       // animator.enabled = false;

        Instantiate(deathParticles, transform.position, transform.rotation);
       Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().damage);
        }
    }
}
