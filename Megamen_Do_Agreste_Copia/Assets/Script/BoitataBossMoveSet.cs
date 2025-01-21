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

    // Verificação por Tag
    public string playerTag = "Player"; // Tag do jogador
    private GameObject player; // Referência ao jogador

    // Segundo Ataque
    public float JumpForce = 7f;
    public float jumpCoolDown = 1.5f;
    private bool PodePular = false;
    private float proximoPulo = 0f;
    private bool trocouPadrão = false;
    public float TempoRecargaTiro = 2f;
    public GameObject PrefabProjetil;
    public Transform PontoDisparo;
    public float TempoVidaProjetil;
    private float tempoUltimoTiro;
    public float velocidadeProjetil = 10f;

    // Terceiro Ataque
    public Transform Molotov1;
    public Transform Molotov2;
    public GameObject Fogo;
    public float TempoDeVidaProjetilNoChao;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pontoAtual = pontoB.position;
        Chamas.SetActive(true);
    }

    void Update()
    {
        // Procura pelo jogador com base na tag
        player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            float distanciaParaJogador = Vector2.Distance(transform.position, player.transform.position);

            if (distanciaParaJogador <= AlcanceParaComeçaAPorrada)
            {
                TrocaDePadroes();
            }
        }
    }

    public void TrocaDePadroes()
    {
        if (Moves <= 4)
        {
            ColocandoFogoEmCriançasECorrendo();
            Chamas.transform.localScale = new Vector3(DistanciaLançaXanas, Chamas.transform.localScale.y, Chamas.transform.localScale.z);
            Chamas.SetActive(true);
        }
        else if (Moves <= 14)
        {
            anim.SetBool("Correndo", false);

            if (!trocouPadrão)
            {
                StartCoroutine(DelayTrocaPadrão());
            }
            else
            {
                Pulando();
                AtirandoEmPaulista();
            }
            Chamas.SetActive(false);
        }
        else if (Moves <= 24)
        {
            if (!trocouPadrão)
            {
                StartCoroutine(DelayTrocaPadrão());
            }
            else
            {
                Pulando();
                Molotov();
            }
        }
        else if (Moves >= 25)
        {
            Moves = 0;
        }
    }

    public void ColocandoFogoEmCriançasECorrendo()
    {
        anim.SetBool("Correndo", true);

        Vector2 novaPosicao = Vector2.MoveTowards(rb.position, pontoAtual, velocidade * Time.fixedDeltaTime);
        rb.MovePosition(novaPosicao);

        if (Vector2.Distance(rb.position, pontoAtual) < 0.1f)
        {
            if (pontoAtual == pontoA.position)
            {
                pontoAtual = pontoB.position;
            }
            else
            {
                pontoAtual = pontoA.position;
            }
            Virar();
            jaContouPonto = false;
        }
    }

    public void Pulando()
    {
        if (player != null && Time.time >= proximoPulo && PodePular)
        {
            Vector2 direcao = (player.transform.position - transform.position).normalized;
            VirarParaJogador();

            Vector2 forcaPulo = new Vector2(direcao.x * velocidade, JumpForce);
            rb.AddForce(forcaPulo, ForceMode2D.Impulse);

            PodePular = false;
            proximoPulo = Time.time + jumpCoolDown;
            anim.SetBool("Pulando", true);
        }
    }

    public void AtirandoEmPaulista()
    {
        if (player != null && Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            GameObject projetil = Instantiate(PrefabProjetil, PontoDisparo.position, Quaternion.identity);
            Vector2 direction = (player.transform.position - PontoDisparo.position).normalized;
            Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
            if (rb != null)
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
        if (player != null && Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            GameObject newFogo = Instantiate(Fogo, Molotov1.position, Quaternion.identity);
            GameObject newFogo2 = Instantiate(Fogo, Molotov2.position, Quaternion.identity);

            Vector2 direction1 = (player.transform.position - Molotov1.position).normalized;
            Vector2 direction2 = (player.transform.position - Molotov2.position).normalized;

            Rigidbody2D FogoRb = newFogo.GetComponent<Rigidbody2D>();
            Rigidbody2D FogoRb2 = newFogo2.GetComponent<Rigidbody2D>();

            if (FogoRb != null && FogoRb2 != null)
            {
                FogoRb.gravityScale = 10;
                FogoRb2.gravityScale = 10;
                FogoRb.velocity = direction1 * velocidadeProjetil;
                FogoRb2.velocity = direction2 * velocidadeProjetil;
            }

            tempoUltimoTiro = Time.time;
            Destroy(newFogo, TempoDeVidaProjetilNoChao);
            Destroy(newFogo2, TempoDeVidaProjetilNoChao);
        }
    }

    void Virar()
    {
        if ((pontoAtual.x < transform.position.x && Flip) || (pontoAtual.x > transform.position.x && !Flip))
        {
            Flip = !Flip;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    void VirarParaJogador()
    {
        if (player != null)
        {
            if (player.transform.position.x < transform.position.x && Flip)
            {
                Flip = false;
                Vector3 escala = transform.localScale;
                escala.x = -Mathf.Abs(escala.x);
                transform.localScale = escala;
            }
            else if (player.transform.position.x > transform.position.x && !Flip)
            {
                Flip = true;
                Vector3 escala = transform.localScale;
                escala.x = Mathf.Abs(escala.x);
                transform.localScale = escala;
            }
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
            anim.SetBool("Pulando", false);
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
        yield return new WaitForSeconds(1f);
        trocouPadrão = true;
    }
}
