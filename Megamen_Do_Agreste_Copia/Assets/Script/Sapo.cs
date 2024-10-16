using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapo : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f; //Distância em que o inimigo começa a seguir o jogador
    public float jumpForce = 5f;
    public int damage = 1;
    public float jumpCooldown = 1.5f; // Tempo entre os saltos

    private Rigidbody2D rb;
    private SistemaDeVida vida;
    private bool podePular = false;
    private float lastJumpTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vida = player.GetComponent<SistemaDeVida>();
    }

    void Update()
    {
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
        }

        if (collision.gameObject.CompareTag("Player"))
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
        }
    }
}
