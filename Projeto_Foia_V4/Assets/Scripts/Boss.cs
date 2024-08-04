using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform Player;
    public float followDistance = 5f; // Distância de seguimento
    public float moveSpeed = 2f; // Velocidade de movimento horizontal
    public float jumpForce = 5f; // Força do pulo
    public float jumpInterval = 2f; // Intervalo entre pulos
    public int Damage = 10;
    public LayerMask groundLayer; // Camada do chão para verificação


    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpInterval; // Inicializa o temporizador de pulo
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if (distanceToPlayer <= followDistance)
        {
            // Move o inimigo na direção do jogador
            Vector2 direction = (Player.position - transform.position).normalized;
            direction.y = 0; // Apenas movimentação horizontal

            // Atualiza a posição horizontal
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            // Verifica se está no chão
            isGrounded = IsGrounded();

            // Gerencia o pulo
            jumpTimer -= Time.deltaTime;
            if (isGrounded && jumpTimer <= 0)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpTimer = jumpInterval; // Reseta o temporizador de pulo
            }
        }
    }

    private bool IsGrounded()
    {
        // Verifica se o boss está tocando o chão usando uma caixa de verificação
        // Ajuste os parâmetros para se adequarem ao tamanho do seu objeto
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f; // Distância da caixa de verificação

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        // Desenha a linha da verificação do chão no editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    }
}
