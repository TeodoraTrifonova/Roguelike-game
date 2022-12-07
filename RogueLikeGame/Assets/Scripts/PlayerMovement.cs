using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 movement;
    //private Animator animator;

    //private PlayerCombat playerCombatScript;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        /*animator = GetComponent<Animator>();  
        playerCombatScript = GetComponent<PlayerCombat>();*/
    }

    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement != Vector2.zero)
        {
            MoveCharacter();
            /*animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isMoving",true);

            if(movement.x > 0)
            {
                playerCombatScript.attackPoint.localPosition = new Vector3(0.13f, -0.05f, 0f);
            }
            if (movement.x < 0)
            {
                playerCombatScript.attackPoint.localPosition = new Vector3(-0.13f, -0.05f, 0f);
            }*/
        }
       /* else
        {
            animator.SetBool("isMoving", false);
        }*/

        void MoveCharacter()
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }


}
