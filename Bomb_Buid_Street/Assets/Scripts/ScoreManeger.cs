using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManeger : MonoBehaviour
{
    // Singleton para acesso global
    public static ScoreManeger Instance { get; private set; }

    // Pontua��o atual
    public int score;

    // Refer�ncia ao texto na interface
    public TextMeshProUGUI scoreText;

    // Configura��o do Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita m�ltiplas inst�ncias
        }
    }

    // Inicializa��o no in�cio do jogo
    private void Start()
    {
        ResetScore(); // Garante que o score inicie corretamente
    }

    // Incrementa a pontua��o
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score Incrementado: " + value);
        UpdateScoreText();
    }

    // Atualiza o texto na interface
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreText n�o foi atribu�do no Inspector!");
        }
    }

    // Reseta a pontua��o para o valor inicial
    public void ResetScore()
    {
        score = 2000; // Valor inicial
        UpdateScoreText();
    }
}
