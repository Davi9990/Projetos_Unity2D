using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cronometro : MonoBehaviour
{
    public float timeRemeining;
    public TextMeshProUGUI timerText;

    private bool timerIsRunning = false;


    void Start()
    {
        if(timerText != null)
        {
            timerIsRunning = true;

        }
        else
        {
            Debug.Log("TextMeshPro não atribuido");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if(timeRemeining > 0)
            {
                timeRemeining -= Time.deltaTime;
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
            int minutes = Mathf.FloorToInt(timeRemeining / 60);
            int seconds = Mathf.FloorToInt(timeRemeining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }


    private void TimerExpired()
    {
        timerIsRunning = false;
        Debug.Log("Tempo Acabado");
        SceneManager.LoadScene("Game Over");
        Destroy(gameObject);
    }
}
