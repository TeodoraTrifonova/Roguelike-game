using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : StateMachineBehaviour
{
    private GameObject boss;
    public static Vector3 bossPosition;
    public static Quaternion bossRotation;
    private GameObject player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = animator.gameObject;
        bossPosition = player.transform.position;
        bossRotation = player.transform.localRotation;
        animator.GetComponent<BossController>().isInvulnerable = true;
    }

  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.GetComponent<CircleCollider2D>().enabled = false;
        boss.transform.SetPositionAndRotation(player.transform.position, player.transform.localRotation);
    }

}
