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
    private float lastAttackTime;

    private Rigidbody2D rb;
    public Rigidbody2D PlayerRb;
    private SistemaDeVida vida;

    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;

    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Caminhando();
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
        else if (distancePlayer <= LinguadaDistance)
        {
            Lambida = true;
            rb.velocity = Vector2.zero;
            Lambendo();
        }
        else
        {
            // Retorna ao seu eixo original
            rb.velocity = Vector2.zero;
        }
    }

    public void Lambendo()
    {
        if (Lambida)
        {
            // Aumenta o tamanho do BoxCollider2D para realizar o ataque
            boxCollider2.size = new Vector2(distanciaLinguada, boxCollider2.size.y);
        }
        else
        {
            // Volta o tamanho do BoxCollider2D ao normal
            boxCollider2.size = new Vector2(0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == boxCollider2 && collision.gameObject.CompareTag("Player"))
        {
            // Obtém o sistema de vida do jogador
            vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                // Aplica dano ao jogador
                vida.vida -= damage;

                // Trava o jogador
                PlayerRb.velocity = Vector2.zero;

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

        lastAttackTime = Time.time;
    }
}
