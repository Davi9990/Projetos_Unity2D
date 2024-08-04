using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;

    [Header("Ground Check Settings")]
    public Collider2D groundCheckCollider;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckGrounded();
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        // Ajusta o flip do sprite baseado na direção do movimento
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Define as animações de acordo com o movimento
        if (moveInput > 0)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("WalkingLeft", false);
        }
        else if (moveInput < 0)
        {
            animator.SetBool("WalkingLeft", true);
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("WalkingLeft", false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) // Verifica se o jogador está no chão
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetTrigger("Jump"); // Ativa a animação de pulo
            Debug.Log("Jumping");
        }
    }

    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = groundCheckCollider.IsTouchingLayers(groundLayer);

        Debug.DrawRay(groundCheckCollider.transform.position, Vector2.down * 0.1f, Color.red);
        Debug.Log($"isGrounded: {isGrounded}");

        if (isGrounded && !wasGrounded)
        {
            animator.ResetTrigger("Jump"); // Desativa a animação de pulo ao tocar o chão
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        Debug.Log("Flipped: " + transform.localScale.x); // Log para verificar o flip
    }
}
