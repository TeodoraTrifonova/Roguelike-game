using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*[SerializeField]
    private Animator animator;*/
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    [SerializeField]
    private GameObject deathParticles;

    void Start()
    {
        currentHealth = maxHealth;
    }

   public void TakeDamage(int damage)
    {
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
        /*animator.SetTrigger("isDead");
        
        GetComponent<Collider2D>().enabled = false;
        animator.enabled = false;*/

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
