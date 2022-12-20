using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<string> correspondingNames;

    [SerializeField]
    private TextMeshProUGUI nameDialogue;

    [SerializeField]
    private TextMeshProUGUI sentenceDialogue;

    private void Start()
    {
        sentences = new Queue<string>();
        correspondingNames = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        sentences.Clear();

        foreach (var name in dialogue.names)
        {
            correspondingNames.Enqueue(name);
        }

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 || correspondingNames.Count == 0)
        {
            EndDialogue();
            if(EndConditions.CompleteEndConditions)
            {
                StartCoroutine(DelayedEnding(1));
            }
            return;
        }

        string sentence = sentences.Dequeue();
        nameDialogue.text = correspondingNames.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator DelayedEnding(float seconds)
    {
        try
        {
            GameObject.Find("FallenWarrior").transform.GetChild(1).gameObject.SetActive(false);
        }
        catch
        {

        }

        yield return new WaitForSeconds(seconds);

        try
        {
            Destroy(GameObject.Find("FallenWarrior"));
        }
        catch
        {

        }

        Instantiate(GameObject.Find("SkeletonDeathParticles"), transform.position, Quaternion.identity);

        yield return new WaitForSeconds(seconds);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().UpdateHealth(-100);

    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceDialogue.text = "";
        foreach (var letter in sentence)
        {
            sentenceDialogue.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void EndDialogue()
    {
        Debug.Log("Ending dialogue!");
        GameObject.Find("DialogueMenu").SetActive(false);
    }
}
