using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capilobo : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f;
    public float LinguadaDistance = 2f;
    public float velocidade = 2f;
    public bool Lambida;
    public float distanciaLinguada;
    public int damage;
    public float TempoAgarrando;
    public float TempoLinguada;

    private float lastAttackTime;

    private Rigidbody2D rb;
    public Rigidbody2D PlayerRb;
    private SistemaDeVida vida;

    public BoxCollider2D boxCollider1; // Colisor normal (para dano)
    public BoxCollider2D boxCollider2; // Colisor da linguada (Trigger)

    private Vector2 initialColliderSize;
    private Vector2 initialColliderOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerRb = player.GetComponent<Rigidbody2D>();

        // Armazena o tamanho e offset inicial do BoxCollider2D para resetá-los depois
        initialColliderSize = boxCollider2.size;
        initialColliderOffset = boxCollider2.offset;
    }

    void Update()
    {
        if (!Lambida) // Garante que o inimigo não se mova durante a linguada
        {
            Caminhando();
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

    private IEnumerator LinguadaRoutine()
    {
        Lambida = true;
        rb.velocity = Vector2.zero; // Para o movimento do inimigo

        // Expande o colisor da linguada
        Vector2 direction = (player.position - transform.position).normalized;
        if (player.position.x > transform.position.x)
        {
            // Expande o colisor para a direita
            boxCollider2.offset = new Vector2(distanciaLinguada / 2, boxCollider2.offset.y);
            boxCollider2.size = new Vector2(distanciaLinguada, boxCollider2.size.y);
        }
        else
        {
            // Expande o colisor para a esquerda
            boxCollider2.offset = new Vector2(-distanciaLinguada / 2, boxCollider2.offset.y);
            boxCollider2.size = new Vector2(distanciaLinguada, boxCollider2.size.y);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Lambida)
        {
            // Obtém o sistema de vida do jogador
            vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                // Aplica dano ao jogador
                vida.vida -= damage;

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

        // Liberta o jogador após o tempo de agarrar
        PlayerRb.constraints = RigidbodyConstraints2D.None; // Libera o jogador
        PlayerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Mantém a rotação congelada, mas libera o movimento

        lastAttackTime = Time.time;

        // Após agarrar o jogador, desativa a linguada e reseta o colisor
        Lambida = false;
        ResetLinguadaCollider();
    }

    // Novo método OnCollisionEnter2D para aplicar dano com o boxCollider1
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == boxCollider1 && collision.gameObject.CompareTag("Player"))
        {
            // Obtém o sistema de vida do jogador
            vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                // Aplica dano ao jogador
                vida.vida -= damage;

                Debug.Log("Dano aplicado ao jogador: " + damage);
            }
        }
    }
}
