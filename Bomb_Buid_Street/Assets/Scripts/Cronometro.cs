using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public float timeRemaining;
    public TextMeshProUGUI timerText;

    private bool timerIsRunning = false;

    void Start()
    {
        if(timerText != null)
        {
            timerIsRunning = true;
            UpdateTimerText();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.Log("TextMeshPro não atribuido");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerIsRunning)
        {
            if(timeRemaining > 0)
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
        //Atualiza o texto do cronometro
        if(timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); ;
        }
    }

    private void TimeExpired()
    {
        timerIsRunning = false;
        //Debug.Log("Tempo Esgotado");
    }
}
