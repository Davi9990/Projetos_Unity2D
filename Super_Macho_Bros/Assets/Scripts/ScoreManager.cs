using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Instância singleton
    public int score = 0; // Pontuação atual
    public TextMeshProUGUI scoreText; // Referência ao TextMeshPro para exibir o score

    private void Awake()
    {
        // Implementa o padrão singleton para o ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: Mantém o ScoreManager entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText(); // Atualiza o texto do score na inicialização
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
    }
}
