using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 1;
    [SerializeField] private float dashSpeed = 5; // Velocidade do dash
    private float xAxis,yAxis;
    private bool isDashing = false; // Vari�vel para controlar se o jogador est� realizando um dash
    private bool facingRight = true;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public static bool isGrounded;

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
        dash();
        Jump();
        CoyoteTimeCheck();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) // Verifica se o jogador pressionou o bot�o de dash e n�o est� atualmente dashing
        {
            isDashing = true;
            Invoke(nameof(StopDash), 0.2f); // Define uma dura��o para o dash (0.2 segundos neste exemplo)
        }
       
    }

    private void Move()
    {
        if (!isDashing) // Se n�o estiver dashing, movimenta normalmente
        {
            rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        }
    }

    void dash()
    {
        if (isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * xAxis, rb.velocity.y); // Aplica a velocidade do dash na dire��o do input horizontal
        }
    }

    void StopDash()
    {
        isDashing = false;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && coyoteTimer > 0) // Verifica se o jogador est� no ch�o e dentro do coyote time
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
            coyoteTimer = coyoteTime; // Reseta o timer do coyote time quando o jogador est� no ch�o
        }
        else
        {
            coyoteTimer -= Time.deltaTime; // Decrementa o timer do coyote time
        }
    }

    void FlipCharacter()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !facingRight)
        {
            Flip();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}