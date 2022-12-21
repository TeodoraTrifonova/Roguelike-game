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
    private static int highScore = 0;

    public int Score { get => score; private set => score = value; }
    public int HighScore { get => highScore; private set => highScore = value; }


    public void IncrementScore(int points)
    {
        Score += points;
    }

    public void ClearScore()
    {
        if(Score >= HighScore)
        {
            HighScore = Score;
            ScoreSaver.SaveHighScore(HighScore);
        }
    
    }

    public void Awake()
    {
        instance = this;
        Score = 0;
        highScore = ScoreSaver.LoadPlayer();
    }
    private void Start()
    {
        scoreText.text = "SCORE: " + Score.ToString();
        highScoreText.text = "HIGHSCORE: " + HighScore.ToString();
    }

    private void Update()
    {
        scoreText.text = "SCORE: "+ Score.ToString();
        highScoreText.text = "HIGHSCORE: " + HighScore.ToString();
    }
}
