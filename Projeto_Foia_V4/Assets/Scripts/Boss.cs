using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform Player;
    public float followDistance = 5f; // Dist�ncia de seguimento
    public float moveSpeed = 2f; // Velocidade de movimento horizontal
    public float jumpForce = 5f; // For�a do pulo
    public float jumpInterval = 2f; // Intervalo entre pulos
    public int Damage = 10;
    public LayerMask groundLayer; // Camada do ch�o para verifica��o


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
            // Move o inimigo na dire��o do jogador
            Vector2 direction = (Player.position - transform.position).normalized;
            direction.y = 0; // Apenas movimenta��o horizontal

            // Atualiza a posi��o horizontal
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            // Verifica se est� no ch�o
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
        // Verifica se o boss est� tocando o ch�o usando uma caixa de verifica��o
        // Ajuste os par�metros para se adequarem ao tamanho do seu objeto
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f; // Dist�ncia da caixa de verifica��o

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        // Desenha a linha da verifica��o do ch�o no editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    }
}
