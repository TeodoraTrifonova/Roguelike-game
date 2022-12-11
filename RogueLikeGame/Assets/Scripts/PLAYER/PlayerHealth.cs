using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;

    [SerializeField] 
    private float maxHealth = 100f;

    [SerializeField]
    private GameObject deathParticles;

    [SerializeField]
    private GameObject gameOverMenu;

    private void Start()
    {    
        health = maxHealth;
    }

    public void UpdateHealth(float mod)
    {
        health += mod;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        else if(health <= 0f)
        {
            health = 0f;
            Die();
        }
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
        gameOverMenu.SetActive(true);
    }
}
