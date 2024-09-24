using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public float timeRemaining = 1200;
    public TextMeshProUGUI timerText;

    private bool timerIsRunning = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {

            }
        }
    }

    private void UpdateTimerText()
    {
        //Atualiza o texto do cronometro
        if(timerText != null)
        {
            timerText.text = Mathf.Round(timeRemaining).ToString();
        }
    }
}
