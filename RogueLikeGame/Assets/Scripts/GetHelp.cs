using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHelp : MonoBehaviour
{
    [SerializeField]
    private GameObject helpMenu;

    private void Start()
    {
        helpMenu.SetActive(false);
    }

    void Update()
    {
        GetComponent<Button_UI>().ClickFunc = () =>
        {
            Pause();
        };
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Resume()
    {
        helpMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    void Pause()
    {
        helpMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
