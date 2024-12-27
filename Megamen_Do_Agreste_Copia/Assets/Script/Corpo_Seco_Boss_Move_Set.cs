using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpo_Seco_Boss_Move_Set : MonoBehaviour
{
    // Primeiro Ataque
    private GameObject jogador;
    public float distanciaParaPorrada = 30f;
    public float velocidade = 13f;
    public float tempoRecargaAtaque = 4f; // Tempo de recarga entre ataques
    public float tempoDeAtaque = 5f; // Duração do período de ataque
    public float delayInicial = 2f; // Tempo antes do início do primeiro ataque

    private float ultimoTempoAtaque;
    private bool estaAtacando = false;
    private bool podeAtacar = false;

    private Rigidbody2D rb;
    private SpriteRenderer render;
    private float tempoAtivo = 0f; // Tempo ativo no estado de ataque
    public int Moves = 0;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador == null)
        {
            Debug.LogError("Jogador não encontrado! Certifique-se de que o jogador tem a tag 'Player'.");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        }

        render = GetComponent<SpriteRenderer>();
        if (render == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no inimigo!");
        }

        StartCoroutine(EsperarParaAtacar());
    }

    void Update()
    {
        if (jogador != null && podeAtacar)
        {
            if (estaAtacando)
            {
                // Fase de ataque
                tempoAtivo += Time.deltaTime;
                if (tempoAtivo >= tempoDeAtaque)
                {
                    // Finaliza a fase de ataque
                    estaAtacando = false;
                    podeAtacar = false;
                    tempoAtivo = 0f;
                    StartCoroutine(RecargaAtaque());
                }
                else
                {
                    ExecutarDash();
                }
            }
        }
    }

    public void TrocaDePadroes()
    {
        if(Moves <= 4)
        {
            ExecutarDash();
        }
        else
        {

        }
    }

    private IEnumerator EsperarParaAtacar()
    {
        yield return new WaitForSeconds(delayInicial);
        podeAtacar = true;
        estaAtacando = true;
    }

    private IEnumerator RecargaAtaque()
    {
        yield return new WaitForSeconds(tempoRecargaAtaque);
        podeAtacar = true;
        estaAtacando = true;
    }

    private void ExecutarDash()
    {
        float distanciaParaJogador = Vector2.Distance(transform.position, jogador.transform.position);

        if (distanciaParaJogador <= distanciaParaPorrada)
        {
            // Move o inimigo na direção do jogador, ignorando o eixo Y
            Vector2 direcao = (jogador.transform.position - transform.position).normalized;
            direcao.y = 0; // Ignora o movimento no eixo Y
            rb.velocity = direcao * velocidade;

            // Ajusta o flip do sprite baseado na direção do movimento
            if (direcao.x > 0 && render.flipX)
            {
                Virar();
            }
            else if (direcao.x < 0 && !render.flipX)
            {
                Virar();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Virar()
    {
        render.flipX = !render.flipX;
    }
}
