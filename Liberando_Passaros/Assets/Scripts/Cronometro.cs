using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Cronometro : MonoBehaviour
{
    public float timeRemaining = 300f;
    public TextMeshProUGUI timeText;
    private bool timerIsRunning = false;


    void Start()
    {
        if(timeText != null)
        {
            timerIsRunning = true;

        }
        else
        {
            Debug.Log("Timer TextMeshPro não está atribuído.");
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
                UpdateTimeText();
            }
            else
            {
                TimeExpired();
            }
        }
    }

    private void UpdateTimeText()
    {
         if(timeText != null)
        {
            timeText.text = Mathf.Round(timeRemaining).ToString() ;
        } 
    }

    private void TimeExpired()
    {
        timerIsRunning = false;
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        timerIsRunning = true;
        UpdateTimeText();
    }
}
