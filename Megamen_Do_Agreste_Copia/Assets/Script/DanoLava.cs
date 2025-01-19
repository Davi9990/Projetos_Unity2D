using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoLava : MonoBehaviour
{
    public SistemaDeVida vida;
    public VidaInimigo inimigo;
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
        if (collision.gameObject.CompareTag("Player") && PodeAtacar ||
            collision.gameObject.CompareTag("EnemySniper") && PodeAtacar)
        {
            AplicarDano(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && PodeAtacar ||
            collision.gameObject.CompareTag("EnemySniper") && PodeAtacar)
        {
            AplicarDano(collision);
        }
    }

    public void AplicarDano(Collision2D collision)
    {
        SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();
        VidaInimigo Inimigo = collision.gameObject.GetComponent<VidaInimigo>(); 

        if(vida != null)
        {
            vida.vida -= damage;
            PodeAtacar = false;
            Debug.Log("Ataque aplicado, cooldawn Iniciado");
            StartCoroutine(RecarregarAtaque());
        }
        else
        {
            Debug.Log("Componente SistemaVida não  encontrado no Jogador");
        }

        if(Inimigo != null)
        {
            Inimigo.currentHealth -= damage;
            PodeAtacar = false;
            Debug.Log("Ataque ao inimigo, cooldawn iniciado");
            StartCoroutine(RecarregarAtaque());
        }
        else
        {
            Debug.Log("Componente de via não encontrado");
        }
    }

    IEnumerator RecarregarAtaque()
    {
        yield return new WaitForSeconds(NextTimeAttack);
        PodeAtacar = true;
        Debug.Log("Cooldawn de atque finalizado");
    }
}
