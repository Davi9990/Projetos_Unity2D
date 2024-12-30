using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpo_Seco_Boss_Move_Set : MonoBehaviour
{
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
    private float tempoParaIncrementar = 0f;

    // Segundo Ataque
    public float tempoRecargaTiro = 1f;
    public GameObject prefabProjetil;
    public Transform pontoDisparo1;
    public Transform pontoDisparo2;
    public Transform pontoDisparo3;
    public float velocidadeProjetil = 10f;
    public float tempoVidaProjetil = 5f;
    private float tempoUltimoTiro;
    public Transform Player;
    private Vector3 pontoDisparoOffset;
    private Vector3 pontoDisparoOffset2;
    private Vector3 pontoDisparoOffset3;

    // Nova variável para controlar quando o boss está parando para atirar
    //private bool pararParaAtirar = false;

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

        pontoDisparoOffset = pontoDisparo1.localPosition;
        pontoDisparoOffset2 = pontoDisparo2.localPosition;
        pontoDisparoOffset3 = pontoDisparo3.localPosition;
    }

    void Update()
    {
        TrocaDePadroes();
    }

    public void TrocaDePadroes()
    {
        if (Moves <= 4)
        {
            ExecutarDash();
            GerenciarAtaque();
            IncrementarMovesSuavemente();
        }
        else if (Moves <= 14)
        {
            ExecutarDash();
            GerenciarAtaque();
            IncrementarMovesSuavemente();
            AtirarNoJogador();
        }
    }

    public void AtirarNoJogador()
    {
        // Verifica se o tempo de recarga de tiro passou e dispara
        if (Time.time > tempoUltimoTiro + tempoRecargaTiro)
        {
           

            GameObject projetil1 = Instantiate(prefabProjetil, pontoDisparo1.position, Quaternion.identity);
            GameObject projetil2 = Instantiate(prefabProjetil, pontoDisparo2.position, Quaternion.identity);
            GameObject projetil3 = Instantiate(prefabProjetil, pontoDisparo3.position, Quaternion.identity);

            Vector2 direcao = (Player.position - pontoDisparo1.position).normalized;
            Vector2 direcao2 = (Player.position - pontoDisparo2.position).normalized;
            Vector2 direcao3 = (Player.position - pontoDisparo3.position).normalized;

            Rigidbody2D rb1 = projetil1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = projetil2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = projetil3.GetComponent<Rigidbody2D>();

            if (rb1 != null)
            {
                rb1.gravityScale = 0;
                rb1.velocity = direcao * velocidadeProjetil;
            }

            if (rb2 != null)
            {
                rb2.gravityScale = 0;
                rb2.velocity = direcao2 * velocidadeProjetil;
            }

            if (rb3 != null)
            {
                rb3.gravityScale = 0;
                rb3.velocity = direcao3 * velocidadeProjetil;
            }

            Destroy(projetil1, tempoVidaProjetil);
            Destroy(projetil2, tempoVidaProjetil);
            Destroy(projetil3, tempoVidaProjetil);

            tempoUltimoTiro = Time.time;
        }
    }

    private void GerenciarAtaque()
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

    private void IncrementarMovesSuavemente()
    {
        tempoParaIncrementar += Time.deltaTime;

        // Evitar pulos estranhos
        if (tempoParaIncrementar >= 2f)
        {
            Moves += 1; // Incrementa Moves indefinidamente
            tempoParaIncrementar -= 2f; // Subtrai o tempo de incremento para continuar suavemente
            Debug.Log($"Moves Incrementado para: {Moves}");
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

        // Ajustar posições dos pontos de disparo com base na direção do flip
        if (render.flipX)
        {
            pontoDisparo1.localPosition = new Vector3(-Mathf.Abs(pontoDisparoOffset.x), pontoDisparoOffset.y, pontoDisparoOffset.z);
            pontoDisparo2.localPosition = new Vector3(-Mathf.Abs(pontoDisparoOffset2.x), pontoDisparoOffset2.y, pontoDisparoOffset2.z);
            pontoDisparo3.localPosition = new Vector3(-Mathf.Abs(pontoDisparoOffset3.x), pontoDisparoOffset3.y, pontoDisparoOffset3.z);
        }
        else
        {
            pontoDisparo1.localPosition = new Vector3(Mathf.Abs(pontoDisparoOffset.x), pontoDisparoOffset.y, pontoDisparoOffset.z);
            pontoDisparo2.localPosition = new Vector3(Mathf.Abs(pontoDisparoOffset2.x), pontoDisparoOffset2.y, pontoDisparoOffset2.z);
            pontoDisparo3.localPosition = new Vector3(Mathf.Abs(pontoDisparoOffset3.x), pontoDisparoOffset3.y, pontoDisparoOffset3.z);
        }
    }
}
