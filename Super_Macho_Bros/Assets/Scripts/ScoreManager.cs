using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Inst�ncia singleton
    public int score = 0; // Pontua��o atual
    public TextMeshProUGUI scoreText; // Refer�ncia ao TextMeshPro para exibir o score

    private void Awake()
    {
        // Implementa o padr�o singleton para o ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: Mant�m o ScoreManager entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText(); // Atualiza o texto do score na inicializa��o
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
