using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue startingDialogue;

    [SerializeField]
    private Dialogue endingDialogue;

    public void TriggerDialogue()
    {
        if(EndConditions.CompleteEndConditions == false)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(startingDialogue);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(endingDialogue);
        }
        // Bi mi haresalo da ima animaciq za popup na dialogue bubble-a
    }
}
