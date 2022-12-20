using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameObject gameOverMenu;
    private GameObject player;


    private void Start()
    {
       
        gameOverMenu = GameObject.Find("GameOverMenu");
        player = GameObject.FindGameObjectWithTag("Player");
        AudioManager.instance.PlayTheme("IngameTheme");
        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        if(player == null)
        {
            gameOverMenu.SetActive(true);
        }
    }
}
