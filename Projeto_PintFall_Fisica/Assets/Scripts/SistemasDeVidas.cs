using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SistemasDeVidas : MonoBehaviour
{
    public int vida;
    public int vidaatual;
    public int vidaMaxima;

    public Image[] Hits;
    public Sprite cheio;
    public Sprite vazio;


    void Start()
    {
        AtualizarHudDeVida();
    }

    // Update is called once per frame
    void Update()
    {
        VerificarMorte();
    }

    public void AtualizarHudDeVida()
    {
        if(vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        if(vidaatual < vida)
        {

        }

        for(int i = 0; i < Hits.Length; i++)
        {
            if(i < vida)
            {
                Hits[i].sprite = cheio;
            }
            else
            {
                Hits[i].sprite = vazio;
            }

            Hits[i].enabled = i < vidaMaxima;
        }
    }

    void VerificarMorte()
    {
        if(vida <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }
}
