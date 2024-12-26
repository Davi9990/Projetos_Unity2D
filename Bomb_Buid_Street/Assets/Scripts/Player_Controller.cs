using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private static Player_Controller instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mant�m o GameObject entre cenas
        }
        else
        {
            Destroy(gameObject); // Evita m�ltiplas inst�ncias
        }
    }
}
