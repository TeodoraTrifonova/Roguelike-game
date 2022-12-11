using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMainMenu()
    {
        ScoreCounter.instance.ClearScore();
        SceneManager.LoadScene("Menu");
    }

    public void DoExitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
