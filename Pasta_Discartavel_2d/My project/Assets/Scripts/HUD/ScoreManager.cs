using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Inst�ncia �nica do ScoreManager

    private int score; // Pontua��o atual
    public TextMeshProUGUI scoreText; // Refer�ncia ao TextMeshProUGUI para exibir a pontua��o

    private void Awake()
    {
        // Configura��o para garantir que exista apenas uma inst�ncia do ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Garante que o ScoreManager n�o seja destru�do ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Destroi a nova inst�ncia se uma j� existir
        }
    }

    private void Start()
    {
        UpdateScoreText(); // Atualiza o texto da pontua��o no in�cio
        SceneManager.sceneLoaded += OnSceneLoaded; // Adiciona um listener para o evento de carregamento de cena
    }

    private void OnDestroy()
    {
        // Remove o listener para o evento de carregamento de cena quando o ScoreManager for destru�do
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // M�todo para adicionar pontos � pontua��o atual
    public void AddScore(int value)
    {
        score += value; // Adiciona o valor � pontua��o
        UpdateScoreText(); // Atualiza o texto da pontua��o
    }

    // M�todo para atualizar o texto da pontua��o exibido
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Atualiza o texto com a pontua��o atual
        }
    }

    // M�todo chamado quando uma nova cena � carregada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetScore(); // Reseta a pontua��o ao carregar uma nova cena
    }

    // M�todo para resetar a pontua��o
    public void ResetScore()
    {
        score = 0; // Zera a pontua��o
        UpdateScoreText(); // Atualiza o texto da pontua��o
    }
}
