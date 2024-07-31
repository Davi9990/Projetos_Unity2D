using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cronometro : MonoBehaviour
{
    public float timeRemaining = 60f; // Tempo inicial em segundos
    public TextMeshProUGUI timerText; // Referência ao TextMeshPro para exibir o cronômetro
    public Barra_de_Vida playerHealth; // Referência ao script de saúde do jogador

    private bool timerIsRunning = false;

    private void Start()
    {
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
}
