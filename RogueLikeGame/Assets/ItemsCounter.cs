using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsCounter : MonoBehaviour
{
    private TMP_Text countingText;

    void Start()
    {
        countingText = GetComponent<TMP_Text>();
        countingText.text = "";
    }

    void Update()
    {
        if(Backpack.ItemsCount <= 0)
        {
            countingText.text = "";
        }
        else
        {
            countingText.text = Backpack.ItemsCount.ToString();
        }
    }
}
