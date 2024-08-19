using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentos_com_dash : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 1;
    [SerializeField] private float dashSpeed = 5; // Velocidade do dash
    private float xAxis;
    private bool isDashing = false; // Variável para controlar se o jogador está realizando um dash

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Coyote Time Settings")]
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGrounded();
        GetInputs();
        Move();
        Dash();
        Jump();
        CoyoteTimeCheck();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Dash") && !isDashing) // Verifica se o jogador pressionou o botão de dash e não está atualmente dashing
        {
            isDashing = true;
            Invoke(nameof(StopDash), 0.2f); // Define uma duração para o dash (0.2 segundos neste exemplo)
        }
    }

    private void Move()
    {
        if (!isDashing) // Se não estiver dashing, movimenta normalmente
        {
            rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        }
    }

    void Dash()
    {
        if (isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * xAxis, rb.velocity.y); // Aplica a velocidade do dash na direção do input horizontal
        }
    }

    void StopDash()
    {
        isDashing = false;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && coyoteTimer > 0) // Verifica se o jogador está no chão e dentro do coyote time
        {
            rb.velocity = Vector2.up * 15;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void CoyoteTimeCheck()
    {
        if (isGrounded)
        {
            coyoteTimer = coyoteTime; // Reseta o timer do coyote time quando o jogador está no chão
        }
        else
        {
            coyoteTimer -= Time.deltaTime; // Decrementa o timer do coyote time
        }
    }

}
