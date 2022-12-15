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
    private GameObject damageParticles;

    [SerializeField]
    private GameObject gameOverMenu;

    public HealthBarController healthBar;

    private void Start()
    {    
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void UpdateHealth(float mod)
    {
        health += mod;
        healthBar.SetHealth(health);

        if(mod < 0)
        {
            Instantiate(damageParticles, transform.position, transform.rotation);
        }

        if (health > maxHealth)
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
        AudioManager.instance.Stop("Ingame theme");
        AudioManager.instance.Play("player death");
        AudioManager.instance.Play("player death theme");
        GetComponent<Collider2D>().enabled = false;
        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
        gameOverMenu.SetActive(true);
    }
}
