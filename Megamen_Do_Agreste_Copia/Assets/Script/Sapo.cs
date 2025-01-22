using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapo : MonoBehaviour
{
    public string playerTag = "Player"; // Tag do jogador
    public float followDistance = 10f; // Distância em que o inimigo começa a seguir o jogador
    public float jumpForce = 5f;
    public int damage = 1;
    public float jumpCooldown = 1.5f; // Tempo entre os saltos

    private Transform player;
    private Rigidbody2D rb;
    private SistemaDeVida vida;
    private bool podePular = false;
    private float lastJumpTime = 0f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EncontrarPlayer(); // Encontra o jogador no início
    }

    void Update()
    {
        // Verifica se o jogador ainda existe
        if (player == null)
        {
            EncontrarPlayer(); // Tenta encontrar o jogador novamente caso tenha sido recriado
            return;
        }

        // Calcula a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Se a distância for menor ou igual ao followDistance e o inimigo puder pular
        if (distanceToPlayer <= followDistance && podePular && Time.time >= lastJumpTime + jumpCooldown)
        {
            // Calcula a direção do pulo em direção ao jogador
            Vector2 jumpDirection = (player.position - transform.position).normalized;

            // Aplica a força de pulo
            rb.velocity = new Vector2(jumpDirection.x, 1) * jumpForce; // Direção horizontal + pulo para cima

            // Registra o tempo do último pulo
            lastJumpTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            podePular = true;
            anim.SetBool("Pulando", true);
        }

        if (collision.gameObject.CompareTag(playerTag))
        {
            if (vida != null)
            {
                vida.vida -= damage; // Diminui a vida do jogador ao colidir com ele
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            podePular = false;
            anim.SetBool("Pulando", false);
        }
    }

    // Função para encontrar o jogador pelo tag
    private void EncontrarPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
            vida = playerObject.GetComponent<SistemaDeVida>();
        }
    }
}
