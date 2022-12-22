using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{
    [SerializeField]
    private TextHelp beginningHelp;
    [SerializeField]
    private TextHelp bossFoundHelp;
    [SerializeField]
    private TextHelp allIngredientsFoundHelp;
    [SerializeField]
    private TextHelp someIngredientsFoundHelp;

    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private Image image;

    private Color bgcolor;

    private void Awake()
    {
        bgcolor = image.color;
    }

    private void Update()
    {
        if(GameStates.Instance.CurrentState == GameStates.State.beginning)
        {
            ChangeText(beginningHelp);
        }
        else if(GameStates.Instance.CurrentState == GameStates.State.bossFound)
        {
            ChangeText(bossFoundHelp);
        }
        else if(GameStates.Instance.CurrentState == GameStates.State.someIngredientsFound)
        {
            ChangeText(someIngredientsFoundHelp);
        }
        else if(GameStates.Instance.CurrentState == GameStates.State.allIngredientsFound)
        {
            ChangeText(allIngredientsFoundHelp);
        }
    }

    private void ChangeText(TextHelp _text)
    {
        if (text.text != _text.text)
        {
            text.text = _text.text;
            StartCoroutine(ChangeColor());
        }
    }

    IEnumerator ChangeColor()
    {
        image.color = Color.green;
        yield return new WaitForSeconds(3);
        image.color = bgcolor;
    }
}
