using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cronometro : MonoBehaviour
{
    public float timeRemaining = 60f; // Tempo inicial em segundos
    public TextMeshProUGUI timerText; // Referência ao TextMeshPro para exibir o cronômetro
    public Barra_de_Vida playerHealth; // Referência ao script de saúde do jogador

    private bool timerIsRunning = false;

    private void Start()
    {
        // Inscreve-se no evento de carregamento de cena
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (timerText != null)
        {
            timerIsRunning = true;
            UpdateTimerText();
        }
        else
        {
            Debug.LogError("Timer TextMeshPro não está atribuído.");
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                TimeExpired();
            }
        }
    }

    private void UpdateTimerText()
    {
        // Atualiza o texto do cronômetro
        if (timerText != null)
        {
            timerText.text = Mathf.Round(timeRemaining).ToString();
        }
    }

    private void TimeExpired()
    {
        timerIsRunning = false;
        // Se o jogador ainda estiver na fase, fazer com que o jogador morra
        if (playerHealth != null)
        {
            playerHealth.Die();
        }
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        timerIsRunning = true;
        UpdateTimerText();
    }

    // Método chamado quando uma nova cena é carregada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer(300f); // Ou qualquer valor padrão que você queira usar
    }

    private void OnDestroy()
    {
        // Desinscreve-se do evento de carregamento de cena para evitar leaks de memória
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
