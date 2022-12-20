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
        if(EndConditions.CompleteEndConditions == false && EndConditions.FailEndConditions == false)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(startingDialogue);
        }
        else if(EndConditions.CompleteEndConditions == true)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(endingDialogue);
        }
        else if(EndConditions.FailEndConditions == true)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(failedDialogue);
        }
        // Bi mi haresalo da ima animaciq za popup na dialogue bubble-a
    }
}
