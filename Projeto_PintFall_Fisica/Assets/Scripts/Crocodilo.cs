using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodilo : MonoBehaviour
{
    public float TempoBocaAberta;
    public float TempoBocaFechado;
    public bool HoradedarDano = false;
    public int Dano;
    //public SistemasDeVidas vida;

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!HoradedarDano)
        {
            TempoBocaFechado += Time.deltaTime;
            anim.SetBool("Abrir", false);
            if (TempoBocaFechado >= 4)
            {
                HoradedarDano = true;
                TempoBocaFechado = 0f;
            }
        }
        else
        {
            TempoBocaAberta += Time.deltaTime;
            anim.SetBool("Abrir", true);
            if (TempoBocaAberta >= 4)
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
            SistemasDeVidas vida = collision.gameObject.GetComponent<SistemasDeVidas>();

            if(vida != null)
            {
                Debug.Log("Está dando Dano");
                vida.vidaatual = vida.vida;
                vida.vida -= Dano;
                vida.AtualizarHudDeVida();
            }
            else
            {
                Debug.LogWarning("Sistema não encontrado no objeto Player");
            }
        }
    }
}
