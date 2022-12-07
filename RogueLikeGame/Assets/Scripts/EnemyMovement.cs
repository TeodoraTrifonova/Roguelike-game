using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float moveSpeed; 
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    [SerializeField]
    private GameObject walkingParticles;

    [SerializeField]
    private GameObject feetPos;

    private float particleTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particleTimer = 0;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void SpawnParticles()
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

    void MoveCharacter(Vector2 direction)
    {
        if(movement != null)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (direction.x > 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);

        }

        SpawnParticles();

        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
