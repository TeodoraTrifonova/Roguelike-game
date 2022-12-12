using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    [SerializeField]
    private float detectionDelay = 0.3f;

    [SerializeField]
    private float detectionRadius = 2f;

    [SerializeField]
    Animator animator;

    [SerializeField]
    private LayerMask detectionMask = 7;

    private bool playerInRadius = false;

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    private void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, detectionRadius, detectionMask);
        if (collider != null)
        {
            Debug.Log("Near fallen warrior!");
            playerInRadius = true;
            animator.SetBool("isOpen", true);
        }
        else
        {
            playerInRadius = false;
            animator.SetBool("isOpen", false);
        }
    }

    private void Update()
    {
        
        if(playerInRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.ClearDeveloperConsole();
                gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
