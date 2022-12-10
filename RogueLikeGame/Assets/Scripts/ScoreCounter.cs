using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
 
    public static ScoreCounter instance;   
    public TextMeshProUGUI scoreText;
    public static int score = 0;

    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();

    }

    private void Update()
    {
        scoreText.text = "SCORE: "+ score.ToString();
      
    }





}
