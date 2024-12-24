using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger Instance { get; private set; }


    public int score;
    public TextMeshProUGUI scoreText;


    private void Start()
    {
        //ResetScore();
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score Incrementado: " + value);
        //Movimentacao.pontuacao = score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score.ToString();
        }
    }

    public void ResetScore()
    {
        score = 2000;
        UpdateScoreText();
    }
}
