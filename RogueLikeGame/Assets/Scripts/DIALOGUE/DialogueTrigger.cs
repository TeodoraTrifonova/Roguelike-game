using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue startingDialogue;

    [SerializeField]
    private Dialogue failedDialogue;

    [SerializeField]
    private Dialogue endingDialogue;

    public void TriggerDialogue()
    {
        if(GameStates.Instance.CurrentState == GameStates.State.beginning)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(startingDialogue);
        }
        else if(GameStates.Instance.CurrentState == GameStates.State.allIngredientsFound)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(endingDialogue);
        }
        else if(GameStates.Instance.CurrentState == GameStates.State.someIngredientsFound)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(failedDialogue);
        }
        // Bi mi haresalo da ima animaciq za popup na dialogue bubble-a
    }
}
