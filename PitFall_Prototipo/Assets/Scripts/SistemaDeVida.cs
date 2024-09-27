using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeVida : MonoBehaviour
{
    public int vida;
    public int vidaMaxima;

    public Image[] Hits;
    public Sprite cheio;
    public Sprite vazio;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthLogic();
        DeadState();
    }

    void HealthLogic()
    {
        if(vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        for(int i = 0; i < Hits.Length; i++)
        {
            if(i< vida)
            {
                Hits[i].sprite = cheio;
            }
            else
            {
                Hits[i].sprite = vazio;
            }

            if(i < vidaMaxima)
            {
                Hits[i].enabled = true;
            }
            else
            {
                Hits[i].enabled = false;
            }
        }
    }

    void DeadState()
    {
        if(vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
