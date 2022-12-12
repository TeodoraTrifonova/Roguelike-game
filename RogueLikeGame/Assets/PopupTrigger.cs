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
    private CameraManager cameraManager;
    private GameObject dialogBoxMenu;
    private DialogueManager dialogueManager;

    public bool PlayerInRadius { get => playerInRadius; }

    

    private void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        dialogBoxMenu = GameObject.Find("DialogueMenu");
        dialogBoxMenu.SetActive(false);
        dialogueManager = FindObjectOfType<DialogueManager>();

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
            cameraManager.SwitchCamera(playerInRadius);
        }
        else
        {
            playerInRadius = false;
            animator.SetBool("isOpen", false);
            cameraManager.SwitchCamera(playerInRadius);
        }
    }

    private void Update()
    {
        
        if(playerInRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(dialogBoxMenu.activeSelf == false)
                {
                    Debug.ClearDeveloperConsole();
                    dialogBoxMenu.SetActive(true);
                    gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    dialogueManager.DisplayNextSentence();
                }
                
            }
        }
        else
        {
            dialogBoxMenu.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
