using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoAcido : MonoBehaviour
{
    public SistemaDeVida Vida;
    public int damage = 1;
    public float NextTimeAttack;
    public bool PodeAtacar;

    void Start()
    {
        PodeAtacar = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTrigggerEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && PodeAtacar)
        {
            AplicarDano(collision);
        }
    }

    

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PodeAtacar)
        {
            AplicarDano(collision);
        }
    }


    public void AplicarDano(Collision2D collision)
    {
        SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();
        
        if (vida != null)
        {
            vida.vida -= damage;
            PodeAtacar = false;
            Debug.Log("Ataque aplicado, cooldawn Iniciado.");
            StartCoroutine (RecarregarAtaque());
        }
        else
        {
            Debug.Log("Componente Sistemadevida não encontrado no jogador");
        }
    }

    IEnumerator RecarregarAtaque()
    {
        yield return new WaitForSeconds(NextTimeAttack);
        PodeAtacar = true;
        Debug.Log("Cooldawn de ataque finalizado");
    }
}
