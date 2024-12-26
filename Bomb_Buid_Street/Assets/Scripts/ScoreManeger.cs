using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManeger : MonoBehaviour
{
    // Singleton para acesso global
    public static ScoreManeger Instance { get; private set; }

    // Pontuação atual
    public int score;

    // Referência ao texto na interface
    public TextMeshProUGUI scoreText;

    // Configuração do Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita múltiplas instâncias
        }
    }

    // Inicialização no início do jogo
    private void Start()
    {
        ResetScore(); // Garante que o score inicie corretamente
    }

    // Incrementa a pontuação
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
            Debug.LogWarning("ScoreText não foi atribuído no Inspector!");
        }
    }

    // Reseta a pontuação para o valor inicial
    public void ResetScore()
    {
        score = 2000; // Valor inicial
        UpdateScoreText();
    }
}
