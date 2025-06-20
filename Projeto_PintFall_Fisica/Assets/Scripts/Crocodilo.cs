using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodilo : MonoBehaviour
{
    public float TempoBocaAberta;
    public float TempoBocaFechado;
    public bool HoradedarDano = false;
    public int Dano;
    public SistemasDeVidas vida;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!HoradedarDano)
        {
            TempoBocaFechado += Time.deltaTime;
            if(TempoBocaFechado >= 4)
            {
                HoradedarDano = true;
                TempoBocaFechado = 0f;
            }
        }
        else
        {
            TempoBocaAberta += Time.deltaTime;
            if(TempoBocaAberta >= 4)
            {
                HoradedarDano = false;
                TempoBocaAberta = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && HoradedarDano == true)
        {
            Debug.Log("Está dando Dano");
            vida.vidaatual = vida.vida;
            vida.vida -= Dano;
            vida.AtualizarHudDeVida();
        }
    }
}
