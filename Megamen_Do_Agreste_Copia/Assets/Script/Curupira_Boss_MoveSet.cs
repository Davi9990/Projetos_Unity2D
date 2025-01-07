using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curupira_Boss_MoveSet : MonoBehaviour
{
    //Primeiro Ataque
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
    public Transform Player;
    public float followDistance = 30f;
    public bool PodePular;
    public float JumpForce = 9f;
    public float jumpCooldown = 1.5f;
    private float lastJumpTime = 0f;
    private Rigidbody2D rb;
    private bool Flip;

    //Segundo Ataque
    public GameObject Lama1, Lama2;
    public Transform PontoDeLama1;
    public Transform PontoDeLama2;
    public bool PodeAtirarLama = false;
    public float VelocidadeLama = 5f;

    //Terceiro Ataque
    public Transform PontoDeDisparoPedra1;
    public Transform PontoDeDisparoPedra2;
    public bool PodeTacarPedra = false;
    public GameObject PrefabPedra;

    public float AlcanceParaComeçarPorrada = 30f;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Virar();
        float distanciaParaJogador = Vector2.Distance(transform.position, Player.position);

        if (distanciaParaJogador <= AlcanceParaComeçarPorrada)
        {
            TrocaDePadroes();
        }
    }

    public void TrocaDePadroes()
    {
        if(Moves <= 4)
        {
            AtirandoEmCorno();
            PulandoParaOInferno();
        }
        else if (Moves <= 14)
        {
            PodeAtirarLama = true;
            PulandoParaOInferno();
        }
        else if(Moves <= 24)
        {
            PodeAtirarLama = false;
            PulandoParaOInferno();
            PodeTacarPedra = true;
        }
        else if(Moves >= 25)
        {
            PodeTacarPedra = false;
            Moves = 0;
        }
    }

    void AtirandoEmCorno()
    {
        // Verificar se pode atirar
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

            // Desativar a animação após o tiro (com um pequeno atraso, se necessário)
            StartCoroutine(DesativarAnimacaoAtirando());

        }
    }

    // Coroutine para desativar a animação
    IEnumerator DesativarAnimacaoAtirando()
    {
        yield return new WaitForSeconds(0.1f); // Ajuste o tempo de acordo com a duração da animação
        anim.SetBool("Atirando", false);
    }

    void PulandoParaOInferno()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if(distanceToPlayer <= followDistance && PodeAtirar && PodePular && Time.time >= lastJumpTime + jumpCooldown)
        {
            anim.SetBool("Pulando", true);

            Vector2 JumpDirection = (Player.position - transform.position).normalized;

            rb.velocity = new Vector2(JumpDirection.x, 1) * JumpForce;

            lastJumpTime = Time.time;
        }
    }

    void BrotandoDoChao(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            if (Time.time > tempoUltimoTiro + TempoDeReacargaTiro)
            {
                GameObject ProjetilLama1 = Instantiate(Lama1, PontoDeLama1.position, Quaternion.identity);
                GameObject ProjetilLama2 = Instantiate(Lama2, PontoDeLama2.position, Quaternion.identity);

                Vector2 direction =  (Player.position - transform.position).normalized;
                Vector2 VelocidadeVertical = new Vector2(direction.x * VelocidadeLama, VelocidadeLama);
                Vector2 VelocidadeVertical2 = new Vector2(-direction.x * VelocidadeLama, VelocidadeLama);

                Rigidbody2D rb1 = ProjetilLama1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = ProjetilLama2.GetComponent<Rigidbody2D>();

                if(rb1 != null && rb2 != null)
                {
                    //Define a gravidade e aplica a velocidade de lançamento para criar a parábola
                    rb1.gravityScale = 1;
                    rb1.velocity = VelocidadeVertical;

                    rb2.gravityScale = 1;
                    rb2.velocity = VelocidadeVertical2;
                }

                Destroy(ProjetilLama1, tempoVidaProjetil);
                Destroy(ProjetilLama2 , tempoVidaProjetil);

                tempoUltimoTiro = Time.time;
            }
        }
    }

    void PegandoPedraETacandoEFumando(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            if(Time.time > tempoUltimoTiro + TempoDeReacargaTiro)
            {
                GameObject ProjetilPedra = Instantiate(PrefabPedra, PontoDeDisparoPedra1.position, Quaternion.identity);
                GameObject ProjetilPedra2 = Instantiate(PrefabPedra, PontoDeDisparoPedra2.position, Quaternion.identity);

                Vector2 direction = (Player.position - transform.position).normalized;
                Vector2 VelocidadeVertical = new Vector2(direction.x * VelocidadeLama, VelocidadeLama);
                Vector2 VelocidadeVertical2 = new Vector2(direction.x * VelocidadeLama, VelocidadeLama);

                Rigidbody2D rb1 = ProjetilPedra.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = ProjetilPedra2.GetComponent<Rigidbody2D>();


                if (rb1 != null && rb2 != null)
                {
                    //Define a gravidade e aplica a velocidade de lançamento para criar a parábola
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

        if (PodeAtirarLama == true)
        {
            BrotandoDoChao(collision.collider);
        }

        if(PodeTacarPedra == true)
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
        if(Player.position.x < transform.position.x && Flip)
        {
            Flip = false;
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x);
            transform.localScale = escala;
        }
        else if(Player.position.x > transform.position.x && !Flip)
        {
            Flip = true;
            Vector3 escala = transform.localScale;
            escala.x = -Mathf.Abs(escala.x);
            transform.localScale = escala;
        }
    }
}
