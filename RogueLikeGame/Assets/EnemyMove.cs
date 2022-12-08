using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : StateMachineBehaviour
{

    Transform player;
    [SerializeField]
    private float moveSpeed = 5f;
    public float attackRange = 3f;


    private Rigidbody2D rb;
    Enemy enemy;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = new Vector2(0, 0); // removes velocity from the enemy

        enemy.LookAtPlayer();

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);

            Vector2 newPos = Vector2.MoveTowards(rb.transform.position, target, moveSpeed * Time.deltaTime);

            // naprawi moveX, moveY

            rb.MovePosition(newPos);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
