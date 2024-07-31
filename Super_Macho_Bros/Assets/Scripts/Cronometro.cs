using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cronometro : MonoBehaviour
{
    public float timeRemaining = 60f; // Tempo inicial em segundos
    public TextMeshProUGUI timerText; // Refer�ncia ao TextMeshPro para exibir o cron�metro
    public Barra_de_Vida playerHealth; // Refer�ncia ao script de sa�de do jogador

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
            Debug.LogError("Timer TextMeshPro n�o est� atribu�do.");
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
        // Atualiza o texto do cron�metro
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
