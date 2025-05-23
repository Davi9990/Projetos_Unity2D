using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curupira_Boss_MoveSet : MonoBehaviour
{
    // Primeiro Ataque
    public float velocidade = 5f;
    public GameObject prefebProjetil;
    public Transform pontoDisparo;
    public float velocidadeDoProjetil = 10f;
    public float tempoVidaProjetil = 5f;
    private SpriteRenderer render;
    public bool PodeAtirar;
    public Transform PontoDePulo;
    public float TempoDeReacargaTiro;
    private float tempoUltimoTiro;
    public float Moves = 0;
    private Transform playerTransform; // Substitui��o da refer�ncia direta ao jogador
    public float followDistance = 30f;
    public bool PodePular;
    public float JumpForce = 9f;
    public float jumpCooldown = 1.5f;
    private float lastJumpTime = 0f;
    private Rigidbody2D rb;
    private bool Flip;

    // Segundo Ataque
    public GameObject Lama1, Lama2;
    public Transform PontoDeLama1;
    public Transform PontoDeLama2;
    public bool PodeAtirarLama = false;
    public float VelocidadeLama = 5f;

    // Terceiro Ataque
    public Transform PontoDeDisparoPedra1;
    public Transform PontoDeDisparoPedra2;
    public bool PodeTacarPedra = false;
    public GameObject PrefabPedra;

    public float AlcanceParaCome�arPorrada = 30f;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Inicializando a refer�ncia do jogador
        AtualizarReferenciaJogador();
    }

    private void OnEnable()
    {
        // Esse m�todo ser� chamado toda vez que o objeto for ativado, garantindo que
        // a refer�ncia ao jogador seja atualizada sempre que o objeto for reabilitado (como ap�s respawn).
        AtualizarReferenciaJogador();
    }

    private void OnDisable()
    {
        // Esse m�todo ser� chamado quando o objeto for desabilitado (como quando o jogador morrer).
        playerTransform = null;
    }

    private void AtualizarReferenciaJogador()
    {
        // Atualiza a refer�ncia do jogador toda vez que for necess�rio.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Jogador atualizado com sucesso.");
        }
        else
        {
            Debug.LogWarning("Jogador n�o encontrado! Certifique-se de que o jogador foi instanciado.");
        }
    }

    void Update()
    {
        // Verifica se o jogador foi encontrado
        if (playerTransform == null)
        {
            // Se n�o encontrou, tenta novamente
            AtualizarReferenciaJogador();
            if (playerTransform == null) return; // Se ainda n�o encontrou, termina a execu��o
        }

        Virar();
        float distanciaParaJogador = Vector2.Distance(transform.position, playerTransform.position);

        if (distanciaParaJogador <= AlcanceParaCome�arPorrada)
        {
            TrocaDePadroes();
        }
    }

    public void TrocaDePadroes()
    {
        if (Moves <= 4)
        {
            AtirandoEmCorno();
            PulandoParaOInferno();
        }
        else if (Moves <= 14)
        {
            PodeAtirarLama = true;
            PulandoParaOInferno();
        }
        else if (Moves <= 24)
        {
            PodeAtirarLama = false;
            PulandoParaOInferno();
            PodeTacarPedra = true;
        }
        else if (Moves >= 25)
        {
            PodeTacarPedra = false;
            Moves = 0;
        }
    }

    void AtirandoEmCorno()
    {
        if (PodeAtirar && Time.time > tempoUltimoTiro + TempoDeReacargaTiro)
        {
            anim.SetBool("Atirando", true);

            GameObject projetil = Instantiate(prefebProjetil, pontoDisparo.position, Quaternion.identity);
            Vector2 direcao = (PontoDePulo.position - pontoDisparo.position).normalized;
            Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1;
                rb.velocity = direcao * velocidadeDoProjetil;
            }
            Destroy(projetil, tempoVidaProjetil);
            tempoUltimoTiro = Time.time;

            StartCoroutine(DesativarAnimacaoAtirando());
        }
    }

    IEnumerator DesativarAnimacaoAtirando()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Atirando", false);
    }

    void PulandoParaOInferno()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= followDistance && PodeAtirar && PodePular && Time.time >= lastJumpTime + jumpCooldown)
        {
            anim.SetBool("Pulando", true);

            Vector2 JumpDirection = (playerTransform.position - transform.position).normalized;

            rb.velocity = new Vector2(JumpDirection.x, 1) * JumpForce;

            lastJumpTime = Time.time;
        }
    }

    void BrotandoDoChao(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chao") && playerTransform != null)
        {
            if (Time.time > tempoUltimoTiro + TempoDeReacargaTiro)
            {
                GameObject ProjetilLama1 = Instantiate(Lama1, PontoDeLama1.position, Quaternion.identity);
                GameObject ProjetilLama2 = Instantiate(Lama2, PontoDeLama2.position, Quaternion.identity);

                Vector2 direction = (playerTransform.position - transform.position).normalized;
                Vector2 VelocidadeVertical = new Vector2(direction.x * VelocidadeLama, VelocidadeLama);
                Vector2 VelocidadeVertical2 = new Vector2(-direction.x * VelocidadeLama, VelocidadeLama);

                Rigidbody2D rb1 = ProjetilLama1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = ProjetilLama2.GetComponent<Rigidbody2D>();

                if (rb1 != null && rb2 != null)
                {
                    rb1.gravityScale = 1;
                    rb1.velocity = VelocidadeVertical;

                    rb2.gravityScale = 1;
                    rb2.velocity = VelocidadeVertical2;
                }

                Destroy(ProjetilLama1, tempoVidaProjetil);
                Destroy(ProjetilLama2, tempoVidaProjetil);

                tempoUltimoTiro = Time.time;
            }
        }
    }

    void PegandoPedraETacandoEFumando(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chao") && playerTransform != null)
        {
            if (Time.time > tempoUltimoTiro + TempoDeReacargaTiro)
            {
                GameObject ProjetilPedra = Instantiate(PrefabPedra, PontoDeDisparoPedra1.position, Quaternion.identity);
                GameObject ProjetilPedra2 = Instantiate(PrefabPedra, PontoDeDisparoPedra2.position, Quaternion.identity);

                Vector2 direction = (playerTransform.position - transform.position).normalized;
                Vector2 VelocidadeVertical = new Vector2(direction.x * VelocidadeLama, VelocidadeLama);
                Vector2 VelocidadeVertical2 = new Vector2(direction.x * VelocidadeLama, VelocidadeLama);

                Rigidbody2D rb1 = ProjetilPedra.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = ProjetilPedra2.GetComponent<Rigidbody2D>();

                if (rb1 != null && rb2 != null)
                {
                    rb1.gravityScale = 1;
                    rb1.velocity = VelocidadeVertical;

                    rb2.gravityScale = 1;
                    rb2.velocity = VelocidadeVertical2;
                }

                Destroy(ProjetilPedra, tempoVidaProjetil);
                Destroy(ProjetilPedra2, tempoVidaProjetil);

                tempoUltimoTiro = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            PodeAtirar = true;
            PodePular = true;
            Moves += 1;

            anim.SetBool("Pulando", false);
        }

        if (PodeAtirarLama)
        {
            BrotandoDoChao(collision.collider);
        }

        if (PodeTacarPedra)
        {
            PegandoPedraETacandoEFumando(collision.collider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            PodeAtirar = false;
            PodePular = false;
        }
    }

    void Virar()
    {
        if (playerTransform == null) return;

        if (playerTransform.position.x < transform.position.x && Flip)
        {
            Flip = false;
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x);
            transform.localScale = escala;
        }
        else if (playerTransform.position.x > transform.position.x && !Flip)
        {
            Flip = true;
            Vector3 escala = transform.localScale;
            escala.x = -Mathf.Abs(escala.x);
            transform.localScale = escala;
        }
    }
}
