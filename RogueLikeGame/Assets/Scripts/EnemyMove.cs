using UnityEngine;

public class EnemyMove : StateMachineBehaviour
{

    Transform player;
    [SerializeField]
    private float moveSpeed = 5f;
    private float attackRange;


    private Rigidbody2D rb;
    Enemy enemy;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        attackRange = enemy.AttackRange;

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

            Vector2 direction = new Vector2(newPos.x - target.x, newPos.y - target.y).normalized * -1;

            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);

            /*if (direction.x < 0)
            {
                animator.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                animator.GetComponent<SpriteRenderer>().flipX = false;
            }*/

            rb.MovePosition(newPos);

            enemy.SpawnParticles();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
