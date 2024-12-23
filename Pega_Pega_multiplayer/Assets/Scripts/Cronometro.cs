using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cronometro : MonoBehaviour
{
    public float timeRemaining;
    public Text timerText;

    private bool timerIsRunning = false;

    void Start()
    {
        if(timerText != null)
        {
            timerIsRunning = true;
            UpdateTimerText();
        }
        else
        {
            Debug.Log("Text não atribuido");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                TimerExpired();
            }
        }
    }

    private void UpdateTimerText()
    {
        if(timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{0:00}", minutes, seconds);
        }
    }

    private void TimerExpired()
    {
        timerIsRunning = false;
    }
}
