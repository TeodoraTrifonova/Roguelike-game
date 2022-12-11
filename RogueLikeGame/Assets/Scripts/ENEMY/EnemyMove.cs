using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : StateMachineBehaviour
{

    GameObject player;

    private Rigidbody2D rb;
    EnemyController enemyController;
    Enemy enemy;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        enemyController = animator.GetComponent<EnemyController>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!player.IsDestroyed() && player!=null)
        {

            rb.velocity = new Vector2(0, 0); // removes velocity from the enemy

            enemyController.LookAtPlayer();

            if (Vector2.Distance(player.transform.position, rb.position) <= enemy.AttackRange)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y);

                Vector2 newPos = Vector2.MoveTowards(rb.transform.position, target, enemy.MoveSpeed * Time.deltaTime);

                Vector2 direction = new Vector2(newPos.x - target.x, newPos.y - target.y).normalized * -1;

                animator.SetFloat("moveX", direction.x);
                animator.SetFloat("moveY", direction.y);

                rb.MovePosition(newPos);

                enemyController.WalkingParticles();
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
