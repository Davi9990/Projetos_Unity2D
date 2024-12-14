using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoitataBossMoveSet : MonoBehaviour
{
    // Primeiro Ataque
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 13f;
    private Vector3 pontoAtual;
    private bool Flip = true;
    private Rigidbody2D rb;
    public int Moves = 0;
    public GameObject Chamas;
    public float DistanciaLançaXanas;
    private bool jaContouPonto = false;
    public float AlcanceParaComeçaAPorrada;

    // Segundo Ataque
    public float JumpForce = 7f; // Ajuste da força do pulo
    public float jumpCoolDown = 1.5f; // Tempo entre pulos
    private bool PodePular = false;
    private float proximoPulo = 0f;
    private bool trocouPadrão = false; // Flag para controlar o delay na troca de padrão
    public Transform jogador; // Referência ao Transform do jogador
    public float TempoRecargaTiro = 2f;
    public GameObject PrefabProjetil;
    public Transform PontoDisparo;
    public float TempoVidaProjetil;
    private float tempoUltimoTiro;
    public float velocidadeProjetil = 10f;

    //Terceiro Ataque
    public Transform Molotov1;
    public Transform Molotov2;
    public GameObject Fogo;
    public float TempoDeVidaProjetilNoChao;
    //public Rigidbody2D ProjetillRb1;
    //public Rigidbody2D ProjetillRb2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //ProjetillRb1 = GetComponent<Rigidbody2D>();
        //ProjetillRb2 = GetComponent<Rigidbody2D>();
        pontoAtual = pontoB.position;
        Chamas.SetActive(true);
    }

    void Update()
    {
        float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);

        if (distanciaParaJogador <= AlcanceParaComeçaAPorrada)
        {
            TrocaDePadroes();
        }
    }

    public void TrocaDePadroes()
    {
        if (Moves <= 4)
        {
            ColocandoFogoEmCriançasECorrendo();
            Chamas.transform.localScale = new Vector3(DistanciaLançaXanas, Chamas.transform.localScale.y, Chamas.transform.localScale.z);
        }
        else if (Moves <= 14)
        {
            if (!trocouPadrão)
            {
                // Delay para o inimigo parar antes de começar a pular
                StartCoroutine(DelayTrocaPadrão());
            }
            else
            {
                Pulando();
                AtirandoEmPaulista();
            }
            Chamas.SetActive(false);
        }
        else if(Moves <= 24)
        {
            if (!trocouPadrão)
            {
                StartCoroutine(DelayTrocaPadrão());
            }
            else
            {
                Pulando();
                Molotov();
                //Moves = 0;
            }
        }
        else if(Moves >= 25)
        {
            Moves = 0;
        }
    }

    public void ColocandoFogoEmCriançasECorrendo()
    {
        // Move o inimigo em direção ao ponto atual
        Vector2 novaPosicao = Vector2.MoveTowards(rb.position, pontoAtual, velocidade * Time.fixedDeltaTime);
        rb.MovePosition(novaPosicao);

        if (Vector2.Distance(rb.position, pontoAtual) < 0.1f)
        {
            // Troca o ponto atual
            if (pontoAtual == pontoA.position)
            {
                pontoAtual = pontoB.position;
            }
            else
            {
                pontoAtual = pontoA.position;
            }
            Virar();
            jaContouPonto = false; // Reseta o contador ao atingir o ponto
        }
    }

    public void Pulando()
    {
        if (Time.time >= proximoPulo && PodePular)
        {
            // Calcular a direção em direção ao jogador
            Vector2 direcao = (jogador.position - transform.position).normalized; // jogador é a referência ao Transform do jogador

            // Virar o inimigo para o lado do jogador antes de pular
            VirarParaJogador();

            // Aplicar a força de pulo na direção do jogador
            Vector2 forcaPulo = new Vector2(direcao.x * velocidade, JumpForce); // Ajuste do valor JumpForce
            rb.AddForce(forcaPulo, ForceMode2D.Impulse);

            PodePular = false; // Desabilita o pulo até aterrissar novamente
            proximoPulo = Time.time + jumpCoolDown; // Define o cooldown do próximo pulo
        }
    }

    public void AtirandoEmPaulista()
    {
        if(Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            GameObject projetil = Instantiate(PrefabProjetil, PontoDisparo.position, Quaternion.identity);
            Vector2 direction = (jogador.position - PontoDisparo.position).normalized;
            Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.gravityScale = 0;
                rb.velocity = direction * velocidadeProjetil;
            }
            Destroy(projetil, TempoVidaProjetil);
            tempoUltimoTiro = Time.time;
        }
    }

    public void Molotov()
    {
        if (Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            GameObject newFogo = Instantiate(Fogo, Molotov1.position, Quaternion.identity);
            GameObject newFogo2 = Instantiate(Fogo, Molotov2.position, Quaternion.identity);

            Vector2 direction1 = (jogador.position - Molotov1.position).normalized;
            Vector2 direction2 = (jogador.position - Molotov2.position).normalized;

            Rigidbody2D FogoRb = newFogo.GetComponent<Rigidbody2D>();
            Rigidbody2D FogoRb2 = newFogo2.GetComponent<Rigidbody2D>();

           if(FogoRb != null && FogoRb2 != null)
           {
                FogoRb.gravityScale = 10;
                FogoRb2.gravityScale = 10;
                FogoRb.velocity = direction1 * velocidadeProjetil;
                FogoRb2.velocity = direction2 * velocidadeProjetil;
           }

           tempoUltimoTiro =  Time.time;
           Destroy(newFogo, TempoDeVidaProjetilNoChao);
           Destroy(newFogo2, TempoDeVidaProjetilNoChao);
        }
    }

    void Virar()
    {
        // Verifica a direção com base no ponto atual
        if ((pontoAtual.x < transform.position.x && Flip) || (pontoAtual.x > transform.position.x && !Flip))
        {
            Flip = !Flip; // Inverte o estado de Flip
            Vector3 escala = transform.localScale;
            escala.x *= -1; // Inverte a escala no eixo X
            transform.localScale = escala;
        }
    }

    void VirarParaJogador()
    {
        // Inverte o flip com base na posição do jogador
        if (jogador.position.x < transform.position.x && Flip) // Jogador está à esquerda
        {
            Flip = false;
            Vector3 escala = transform.localScale;
            escala.x = -Mathf.Abs(escala.x); // Garante que o inimigo vire para a esquerda
            transform.localScale = escala;
        }
        else if (jogador.position.x > transform.position.x && !Flip) // Jogador está à direita
        {
            Flip = true;
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x); // Garante que o inimigo vire para a direita
            transform.localScale = escala;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("PontoA") || collision.gameObject.CompareTag("PontoB")) && !jaContouPonto)
        {
            Moves += 1;
            jaContouPonto = true; // Marca como contado
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            PodePular = true; // Habilita pulo ao tocar no chão
            Moves += 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            PodePular = false; // Desabilita pulo ao sair do chão
        }
    }

    private IEnumerator DelayTrocaPadrão()
    {
        // Atrasar a troca de padrão para evitar que o inimigo pule imediatamente
        yield return new WaitForSeconds(1f); // Tempo de delay (1 segundo)
        trocouPadrão = true;
    }
}
