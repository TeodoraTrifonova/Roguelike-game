using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : StateMachineBehaviour
{
 // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
