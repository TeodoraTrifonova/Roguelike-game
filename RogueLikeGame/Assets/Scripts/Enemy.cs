using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
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
        animator.SetTrigger("isDead");
     

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
