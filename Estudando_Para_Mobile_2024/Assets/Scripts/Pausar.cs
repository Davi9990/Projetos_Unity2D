using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausar : MonoBehaviour
{
    bool IsPause;
    void Start()
    {
        IsPause = false;
        Time.timeScale = 1;
    }

    public void GamePause()
    {
        IsPause = !IsPause;

        if(IsPause)
            Time.timeScale = 0;
        if (!IsPause)
            Time.timeScale = 1;
    }

    void OnApplicationPause()
    {
        IsPause = true;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
