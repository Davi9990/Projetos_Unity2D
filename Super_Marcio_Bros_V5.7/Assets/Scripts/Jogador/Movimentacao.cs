using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private Animator animator;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isJumping;
    private bool isGrounded;
    private bool jumpRequest;

    private static Movimentacao instance;

    private void Awake()
    {
        // Implementar Singleton para garantir uma única instância
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Atualiza o estado do jogador se está no chão
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);

        // Atualiza os parâmetros do Animator
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));

        // Verifica se o botão de pulo foi pressionado
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }

        // Reduz a velocidade de subida ao soltar o botão de pulo
        if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y > 0)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (jumpRequest && isGrounded)
        {
            JumpPlayer();
            jumpRequest = false;
        }

        // Atualiza o parâmetro IsJumping do Animator
        animator.SetBool("IsJumping", !isGrounded);
    }

    void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.velocity = new Vector2(xMove * speed, rbPlayer.velocity.y);

        if (xMove > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (xMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void JumpPlayer()
    {
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpForce);
    }
}
