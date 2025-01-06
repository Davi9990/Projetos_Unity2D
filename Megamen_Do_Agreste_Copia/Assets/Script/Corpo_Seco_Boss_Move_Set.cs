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

    // Terceiro Ataque
    public GameObject Alma;
    public Transform PontoDeInvocacao1, PontoDeInvocacao2, PontoDeInvocacao3, PontoDeInvocacao4, PontoDeInvocacao5, PontoDeInvocacao6;
    public bool PodeInvocarAlmas = false;
    public float velocidadeAlma = 5f;
    public float followDistance = 30f;
    public bool PodePular;
    public float lastJumpTime = 0f;
    public float jumpCoolDown = 1.5f;
    public float JumpForce = 9f;
    public float TempoDeRecarga2 = 2f;

    //Quarto Ataque
    public bool PodeInvocar = false;
    public float tempoUltimaInvocacao;
    public float TempoDeRecargaInvocar;
    public Transform PontoCripta1, PontoCripta2;
    public GameObject Minions;
    private Vector3 pontoCriptaOffset1;
    private Vector3 pontoCriptaOffset2;

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

        pontoCriptaOffset1 = PontoCripta1.localPosition;
        pontoCriptaOffset2 = PontoCripta2.localPosition;
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
            velocidade = 4;
        }
        else if(Moves <= 24)
        {
            IncrementarMovesSuavemente();
            velocidade = 5;
            PodeInvocarAlmas = true;
            PulandoPerseguindo();
        }
        else if(Moves <= 34)
        {
            PodeInvocarAlmas = false;
            PulandoPerseguindo();
            PodeInvocar = true;
        }
        else if(Moves >= 35)
        {
            PodeInvocar = false;
            Moves = 0;
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

    void InvocandoAlmasDoChao(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            if(Time.time > tempoUltimoTiro + TempoDeRecarga2)
            {
                // Instanciação dos projéteis
                GameObject[] projeteis = new GameObject[6];
                projeteis[0] = Instantiate(Alma, PontoDeInvocacao1.position, Quaternion.identity);
                projeteis[1] = Instantiate(Alma, PontoDeInvocacao2.position, Quaternion.identity);
                projeteis[2] = Instantiate(Alma, PontoDeInvocacao3.position, Quaternion.identity);
                projeteis[3] = Instantiate(Alma, PontoDeInvocacao4.position, Quaternion.identity);
                projeteis[4] = Instantiate(Alma, PontoDeInvocacao5.position, Quaternion.identity);
                projeteis[5] = Instantiate(Alma, PontoDeInvocacao6.position, Quaternion.identity);

                // Força para propulsão vertical
                float forcaPropulsao = 20f;

                foreach (GameObject projetil in projeteis)
                {
                    Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        // Zera a velocidade inicial e aplica força para cima
                        rb.velocity = Vector2.zero;
                        rb.AddForce(Vector2.up * forcaPropulsao, ForceMode2D.Impulse);
                    }

                    // Destrói o projétil após o tempo de vida
                    Destroy(projetil, tempoVidaProjetil);
                }

                // Atualiza o tempo do último tiro
                tempoUltimoTiro = Time.time;
            }
        }
    }

    void PulandoPerseguindo()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if(distanceToPlayer <= followDistance && PodePular && Time.time >= lastJumpTime + jumpCoolDown)
        {
            Vector2 JumpDirection = (Player.position - transform.position).normalized;

            rb.velocity = new Vector2(JumpDirection.x, 1) * JumpForce;

            lastJumpTime = Time.time;
        }
    }

    void InvocandoDemons(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Chao"))
        {
            Moves += 1;

            if (Time.time > tempoUltimaInvocacao + TempoDeRecargaInvocar)
            {
                GameObject Cabrunco1 = Instantiate(Minions, PontoCripta1.position, Quaternion.identity);
                GameObject Cabrunco2 = Instantiate(Minions, PontoCripta2.position, Quaternion.identity);

                tempoUltimaInvocacao = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            PodePular = true;
        }

        if(PodeInvocarAlmas == true)
        {
            InvocandoAlmasDoChao(collision.collider);
        }

        if(PodeInvocar == true)
        {
            InvocandoDemons(collision.collider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            PodePular = false;
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

            PontoCripta1.localPosition = new Vector3(-Mathf.Abs(pontoCriptaOffset1.x), pontoCriptaOffset1.y, pontoCriptaOffset1.z);
            PontoCripta2.localPosition = new Vector3(-Mathf.Abs(pontoCriptaOffset2.x), pontoCriptaOffset2.y, pontoCriptaOffset2.z);

        }
        else
        {
            pontoDisparo1.localPosition = new Vector3(Mathf.Abs(pontoDisparoOffset.x), pontoDisparoOffset.y, pontoDisparoOffset.z);
            pontoDisparo2.localPosition = new Vector3(Mathf.Abs(pontoDisparoOffset2.x), pontoDisparoOffset2.y, pontoDisparoOffset2.z);
            pontoDisparo3.localPosition = new Vector3(Mathf.Abs(pontoDisparoOffset3.x), pontoDisparoOffset3.y, pontoDisparoOffset3.z);

            PontoCripta1.localPosition = new Vector3(Mathf.Abs(PontoCripta1.localPosition.x), PontoCripta1.localPosition.y, PontoCripta1.localPosition.z);
            PontoCripta2.localPosition = new Vector3(Mathf.Abs(PontoCripta2.localPosition.x), PontoCripta2.localPosition.y, PontoCripta2.localPosition.z);
        }
    }
}
