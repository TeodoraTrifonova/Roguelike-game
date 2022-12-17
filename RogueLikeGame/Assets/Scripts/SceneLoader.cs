using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartTheGame()
    {
        AudioManager.instance.Play("Button click");
        AudioManager.instance.Stop("Main menu background track");
        SceneManager.LoadScene("test");

        
    }

    private void Start()
    {
        AudioManager.instance.PlayTheme("Main menu background track");
        //FindObjectOfType<AudioManager>().Play("Main menu background track");// plays background music in menu

    }
    public void ReturnToMainMenu()
    {
        ScoreCounter.instance.ClearScore();
        SceneManager.LoadScene("Menu");
    }

    public void DoExitGame()
    {
        AudioManager.instance.Play("Button click");
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
