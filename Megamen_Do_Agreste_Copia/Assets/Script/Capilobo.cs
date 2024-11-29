using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capilobo : Todos
{
    public Transform player;
    public float followDistance = 10f;
    public float LinguadaDistance;
    public float velocidade = 2f;
    public bool Lambida;
    public float distanciaLinguada;
    public int damage;
    public float TempoAgarrando;
    public float TempoLinguada;

    private float lastAttackTime;
    public Rigidbody2D PlayerRb;
    private SistemaDeVida vid;

    public BoxCollider2D boxCollider1; // Colisor normal (para dano)
    public BoxCollider2D boxCollider2; // Colisor da linguada (Trigger)
    
    // Refer�ncia para o objeto vazio que servir� de guia
    public Transform GuiaLinguada;

    private Vector2 initialColliderSize;
    private Vector2 initialColliderOffset;

    private bool facingRight = true; // Controla a dire��o que o inimigo est� virado

    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        PlayerRb = player.GetComponent<Rigidbody2D>();
        anim.SetBool("Linguada", false);

        // Armazena o tamanho e offset inicial do BoxCollider2D para reset�-los depois
        initialColliderSize = boxCollider2.size;
        initialColliderOffset = boxCollider2.offset;
    }

    void Update()
    {
        if (!Lambida) // Garante que o inimigo n�o se mova durante a linguada
        {
            Caminhando();
        }
    }

    public void Caminhando()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.position);

        // Inimigo persegue o jogador dentro da dist�ncia de persegui��o
        if (distancePlayer < followDistance && distancePlayer > LinguadaDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * velocidade;

            anim.SetBool("Andando", true);

            // Verifica se precisa virar para a esquerda ou direita
            if (player.position.x > transform.position.x && facingRight) // Player � direita e inimigo virado para a esquerda
            {
                Flip(); // Vira para a direita
            }
            else if (player.position.x < transform.position.x && !facingRight) // Player � esquerda e inimigo virado para a direita
            {
                Flip(); // Vira para a esquerda
            }
        }
        // O inimigo tenta dar a "linguada" se o jogador estiver na dist�ncia adequada
        else if (distancePlayer <= LinguadaDistance && !Lambida)
        {
            StartCoroutine(LinguadaRoutine());
        }
        else
        {
            // Retorna ao seu eixo original
            rb.velocity = Vector2.zero;

            // Reseta o colisor da linguada e a vari�vel Lambida
            ResetLinguadaCollider();
            Lambida = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverte a escala no eixo X para fazer o flip
        transform.localScale = scale;
    }

    private IEnumerator LinguadaRoutine()
    {
        Lambida = true;
        rb.velocity = Vector2.zero; // Para o movimento do inimigo
        anim.SetBool("Andando", false);
        anim.SetBool("Linguada", true);

        // Ajusta o tamanho do colisor de acordo com a dist�ncia
        boxCollider2.size = new Vector2(distanciaLinguada, boxCollider2.size.y);

        // Ajusta o offset de acordo com a dire��o do inimigo
        if (!facingRight)
        {
            boxCollider2.offset = new Vector2(distanciaLinguada / 2, boxCollider2.offset.y); // Para a direita
        }
        else
        {
            boxCollider2.offset = new Vector2(-distanciaLinguada / 2, boxCollider2.offset.y); // Para a esquerda
        }

        // Aguarda o tempo da linguada
        yield return new WaitForSeconds(TempoLinguada);

        // Reseta o colisor e desativa o estado de linguada
        ResetLinguadaCollider();
        Lambida = false;
    }

    private void ResetLinguadaCollider()
    {
        // Reseta o tamanho e o offset do BoxCollider2D de linguada para o estado inicial
        boxCollider2.size = initialColliderSize;
        boxCollider2.offset = initialColliderOffset;

        anim.SetBool("Andando", true);
        anim.SetBool("Linguada", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Lambida)
        {
            // Obt�m o sistema de vida do jogador
            vid = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                // Aplica dano ao jogador
                vid.vida -= damage;

                // Trava completamente o jogador
                PlayerRb.velocity = Vector2.zero;
                PlayerRb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela o jogador completamente

                // Inicia a corrotina de agarrar o jogador
                StartCoroutine(AgarrandoJogador());
            }
        }
    }

    private IEnumerator AgarrandoJogador()
    {
        // Trava o jogador por um tempo
        Debug.Log("Agarrando o Jogador");
        yield return new WaitForSeconds(TempoAgarrando);

        // Liberta o jogador ap�s o tempo de agarrar
        PlayerRb.constraints = RigidbodyConstraints2D.None; // Libera o jogador
        PlayerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Mant�m a rota��o congelada, mas libera o movimento

        lastAttackTime = Time.time;

        // Ap�s agarrar o jogador, desativa a linguada e reseta o colisor
        Lambida = false;
        ResetLinguadaCollider();
    }

    // Novo m�todo OnCollisionEnter2D para aplicar dano com o boxCollider1
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == boxCollider1 && collision.gameObject.CompareTag("Player"))
        {
            // Obt�m o sistema de vida do jogador
            vid = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                // Aplica dano ao jogador
                vid.vida -= damage;

                Debug.Log("Dano aplicado ao jogador: " + damage);
            }
        }
    }
}
