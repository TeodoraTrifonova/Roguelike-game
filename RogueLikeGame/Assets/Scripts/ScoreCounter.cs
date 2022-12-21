using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
 
    public static ScoreCounter instance;   
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private static int score = 0;
    private static int highScore;

    public int Score { get => score; private set => score = value; }


    public void IncrementScore(int points)
    {
        Score += points;
    }

    public void ClearScore()
    {
        Score = 0;
    }

    public void Awake()
    {
        instance = this;
        highScore = ScoreSaver.LoadPlayer();
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
