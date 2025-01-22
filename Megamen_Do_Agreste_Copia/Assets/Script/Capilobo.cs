using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capilobo : Todos
{
    public string playerTag = "Player"; // Tag do jogador
    public float followDistance = 10f;
    public float LinguadaDistance;
    public float velocidade = 2f;
    public bool Lambida;
    public float distanciaLinguada;
    public int damage;
    public float TempoAgarrando;
    public float TempoLinguada;

    private Transform player; // Referência ao transform do jogador
    private float lastAttackTime;
    public Rigidbody2D PlayerRb;
    private SistemaDeVida vid;

    public BoxCollider2D boxCollider1; // Colisor normal (para dano)
    public BoxCollider2D boxCollider2; // Colisor da linguada (Trigger)

    public Transform GuiaLinguada;

    private Vector2 initialColliderSize;
    private Vector2 initialColliderOffset;

    private bool facingRight = true; // Controla a direção que o inimigo está virado

    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Inicializa o jogador
        AtualizarReferenciaPlayer();

        anim.SetBool("Linguada", false);

        // Armazena o tamanho e offset inicial do BoxCollider2D para resetá-los depois
        initialColliderSize = boxCollider2.size;
        initialColliderOffset = boxCollider2.offset;
    }

    void Update()
    {
        if (player == null) // Verifica se o jogador foi perdido (provavelmente devido à morte)
        {
            AtualizarReferenciaPlayer();
            return; // Sai para evitar erros enquanto o jogador não é encontrado
        }

        if (!Lambida) // Garante que o inimigo não se mova durante a linguada
        {
            Caminhando();
        }
    }

    private void AtualizarReferenciaPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
            PlayerRb = playerObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado!");
        }
    }

    public void Caminhando()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.position);

        // Inimigo persegue o jogador dentro da distância de perseguição
        if (distancePlayer < followDistance && distancePlayer > LinguadaDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * velocidade;

            anim.SetBool("Andando", true);

            // Verifica se precisa virar para a esquerda ou direita
            if (player.position.x > transform.position.x && facingRight) // Player à direita e inimigo virado para a esquerda
            {
                Flip(); // Vira para a direita
            }
            else if (player.position.x < transform.position.x && !facingRight) // Player à esquerda e inimigo virado para a direita
            {
                Flip(); // Vira para a esquerda
            }
        }
        // O inimigo tenta dar a "linguada" se o jogador estiver na distância adequada
        else if (distancePlayer <= LinguadaDistance && !Lambida)
        {
            StartCoroutine(LinguadaRoutine());
        }
        else
        {
            // Retorna ao seu eixo original
            rb.velocity = Vector2.zero;

            // Reseta o colisor da linguada e a variável Lambida
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

        // Ajusta o tamanho do colisor de acordo com a distância
        boxCollider2.size = new Vector2(distanciaLinguada, boxCollider2.size.y);

        // Ajusta o offset de acordo com a direção do inimigo
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
        boxCollider2.size = initialColliderSize;
        boxCollider2.offset = initialColliderOffset;

        anim.SetBool("Andando", true);
        anim.SetBool("Linguada", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && Lambida)
        {
            vid = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vid != null)
            {
                vid.vida -= damage;

                PlayerRb.velocity = Vector2.zero;
                PlayerRb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela o jogador completamente

                StartCoroutine(AgarrandoJogador());
            }
        }
    }

    private IEnumerator AgarrandoJogador()
    {
        Debug.Log("Agarrando o Jogador");
        yield return new WaitForSeconds(TempoAgarrando);

        PlayerRb.constraints = RigidbodyConstraints2D.None; // Libera o jogador
        PlayerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        lastAttackTime = Time.time;

        Lambida = false;
        ResetLinguadaCollider();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == boxCollider1 && collision.gameObject.CompareTag(playerTag))
        {
            vid = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vid != null)
            {
                vid.vida -= damage;

                Debug.Log("Dano aplicado ao jogador: " + damage);
            }
        }
    }
}
