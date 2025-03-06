using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouCagaoVouBotarNaMusica : MonoBehaviour
{
    public static SouCagaoVouBotarNaMusica Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
