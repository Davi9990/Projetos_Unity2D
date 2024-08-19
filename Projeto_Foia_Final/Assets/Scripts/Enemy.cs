using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int dano = 1; // Dano que o inimigo causa ao jogador (ajustado para 1)
    public float tempoRecargaAtaque = 3f; // Tempo de recarga entre ataques
    private bool podeAtacar = true; // Controla se o inimigo pode atacar
    private Rigidbody2D rb;
    private SpriteRenderer renderizadorSprite; // Refer�ncia ao SpriteRenderer para o flip
    public LayerMask Dano;

     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D n�o encontrado no inimigo!");
        }

        renderizadorSprite = GetComponent<SpriteRenderer>();
        if (renderizadorSprite == null)
        {
            Debug.LogError("SpriteRenderer n�o encontrado no inimigo!");
        }
    }

    void Update()
    {
       
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador") && podeAtacar)
        {
            AplicarDano(colisao);
        }
    }

    void OnCollisionStay2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador") && podeAtacar)
        {
            AplicarDano(colisao);
        }
    }

    void AplicarDano(Collision2D colisao)
    {
        HealthSystem vida = colisao.gameObject.GetComponent<HealthSystem>();
        MovimentoPlayer movimento = colisao.gameObject.GetComponent<MovimentoPlayer>();
        if (vida != null && movimento != null)
        {
            Debug.Log("Player alcan�ado");
            vida.ReceberDano(dano);
            movimento.ReduzirVelocidade(0.5f, 1f); // Reduz a velocidade do jogador pela metade por 1 segundo
            podeAtacar = false;
            Debug.Log("Ataque aplicado, cooldown iniciado.");
            StartCoroutine(RecargaAtaque());
        }
        else
        {
            if (vida == null)
            {
                Debug.LogError("Componente HealthSystem n�o encontrado no jogador.");
            }
            if (movimento == null)
            {
                Debug.LogError("Componente MovimentoPlayer n�o encontrado no jogador.");
            }
        }
    }

    IEnumerator RecargaAtaque()
    {
        yield return new WaitForSeconds(tempoRecargaAtaque);
        podeAtacar = true;
        Debug.Log("Cooldown de ataque finalizado.");
    }
}
