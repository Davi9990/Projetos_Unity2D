using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger Instance { get; private set; }

    public int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        // Garantir Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // opcional, se quiser manter entre cenas
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        PontuacaoFinal();
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score incrementado: " + value);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void ResetScore(int newScore = 0)
    {
        score = newScore;
        UpdateScoreText();
    }

    public void PontuacaoFinal()
    {
        if(score == 3000)
        {
            SceneManager.LoadScene("Tela de Vitoria");
            Destroy(gameObject);
        }
    }
}
