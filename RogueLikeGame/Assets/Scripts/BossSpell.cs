using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : StateMachineBehaviour
{
    private GameObject boss;
    private GameObject player;
    private bool justOnce = true;
    private bool justOnce2 = true;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        BossController.stackCounter = animator.GetInteger("BossStacks");
        boss.transform.SetPositionAndRotation(player.transform.position + new Vector3(0f, 2f, 0f), player.transform.localRotation);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float nTime = animStateInfo.normalizedTime;
        Debug.Log("nTime:" + nTime);
        if(justOnce && nTime > 1.0f)
        {
            boss.transform.SetPositionAndRotation(player.transform.position + new Vector3(0f,2f,0f), player.transform.localRotation);
            animator.SetInteger("BossStacks", --BossController.stackCounter);
            justOnce = false;
        }
        if (justOnce2 && nTime > 2.0f)
        {
            boss.transform.SetPositionAndRotation(player.transform.position + new Vector3(0f, 2f, 0f), player.transform.localRotation);
            animator.SetInteger("BossStacks", --BossController.stackCounter);
            justOnce2 = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BossController>().isInvulnerable = false;
        animator.SetInteger("BossStacks", 0);
        BossController.stackCounter = 0;
        justOnce = true;
        justOnce2 = true;
    }


}
