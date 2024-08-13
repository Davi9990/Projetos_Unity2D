using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Instância única do ScoreManager

    private int score; // Pontuação atual
    public TextMeshProUGUI scoreText; // Referência ao TextMeshProUGUI para exibir a pontuação

    private void Awake()
    {
        // Configuração para garantir que exista apenas uma instância do ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Garante que o ScoreManager não seja destruído ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Destroi a nova instância se uma já existir
        }
    }

    private void Start()
    {
        UpdateScoreText(); // Atualiza o texto da pontuação no início
        SceneManager.sceneLoaded += OnSceneLoaded; // Adiciona um listener para o evento de carregamento de cena
    }

    private void OnDestroy()
    {
        // Remove o listener para o evento de carregamento de cena quando o ScoreManager for destruído
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método para adicionar pontos à pontuação atual
    public void AddScore(int value)
    {
        score += value; // Adiciona o valor à pontuação
        UpdateScoreText(); // Atualiza o texto da pontuação
    }

    // Método para atualizar o texto da pontuação exibido
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Atualiza o texto com a pontuação atual
        }
    }

    // Método chamado quando uma nova cena é carregada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetScore(); // Reseta a pontuação ao carregar uma nova cena
    }

    // Método para resetar a pontuação
    public void ResetScore()
    {
        score = 0; // Zera a pontuação
        UpdateScoreText(); // Atualiza o texto da pontuação
    }
}
