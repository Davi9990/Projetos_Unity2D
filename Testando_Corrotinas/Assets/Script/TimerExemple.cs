using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerExemple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownTimer(3));
    }

   IEnumerator CountdownTimer(int seconds)
    {
        while(seconds > 0)
        {
            Debug.Log($"Tempo restente: {seconds}s");
            yield return new WaitForSeconds(1); //Espere 1 segundo
            seconds--;
        }
        Debug.Log("O tempo acabou!");
    }
}
