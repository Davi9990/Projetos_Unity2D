using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private Rigidbody2D rbPlayer; // Referência ao componente Rigidbody2D do jogador
    private Animator animator; // Referência ao componente Animator do jogador

    [SerializeField] private float speed = 5f; // Velocidade de movimento do jogador
    [SerializeField] private float jumpForce = 15f; // Força do pulo do jogador
    [SerializeField] private Transform groundCheck; // Ponto de referência para verificar se o jogador está no chão
    [SerializeField] private LayerMask groundLayer; // Máscara de camada para identificar o que é considerado chão

    private bool isJumping; // Verifica se o jogador está pulando
    private bool isGrounded; // Verifica se o jogador está no chão
    private bool jumpRequest; // Indica se foi feita uma solicitação de pulo

    private static Movimentacao instance; // Instância única da classe para implementar o padrão Singleton

    private void Awake()
    {
        // Implementar Singleton para garantir uma única instância da classe Movimentacao
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroi a instância duplicada, mantendo apenas uma
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Não destrói o objeto ao carregar uma nova cena

        rbPlayer = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D anexado ao jogador
        animator = GetComponent<Animator>(); // Obtém o componente Animator anexado ao jogador
    }

    private void Update()
    {
        // Verifica se o jogador está no chão usando um Linecast entre a posição do jogador e o ponto de groundCheck
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);

        // Atualiza o parâmetro "IsGrounded" no Animator para refletir se o jogador está no chão
        animator.SetBool("IsGrounded", isGrounded);
        // Atualiza o parâmetro "Speed" no Animator para refletir a velocidade horizontal do jogador
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));

        // Verifica se o jogador pressionou o botão de pulo e se ele está no chão
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true; // Solicita um pulo
        }

        // Se o botão de pulo for solto e o jogador estiver subindo, reduz a velocidade de subida
        if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y > 0)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f); // Reduz a velocidade vertical em 50%
        }
    }

    private void FixedUpdate()
    {
        Move(); // Chama o método de movimentação

        // Se há uma solicitação de pulo e o jogador está no chão, realiza o pulo
        if (jumpRequest && isGrounded)
        {
            JumpPlayer(); // Chama o método para pular
            jumpRequest = false; // Reseta a solicitação de pulo
        }

        // Atualiza o parâmetro "IsJumping" no Animator para refletir se o jogador está no ar
        animator.SetBool("IsJumping", !isGrounded);
    }

    // Método para controlar a movimentação horizontal do jogador
    void Move()
    {
        float xMove = Input.GetAxis("Horizontal"); // Obtém o input horizontal (esquerda/direita)
        rbPlayer.velocity = new Vector2(xMove * speed, rbPlayer.velocity.y); // Define a velocidade do jogador

        // Ajusta a rotação do jogador com base na direção do movimento
        if (xMove > 0)
        {
            transform.eulerAngles = new Vector2(0, 0); // Se estiver movendo para a direita, mantém a rotação normal
        }
        else if (xMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180); // Se estiver movendo para a esquerda, vira o jogador
        }
    }

    // Método para realizar o pulo do jogador
    void JumpPlayer()
    {
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpForce); // Aplica uma força vertical para o pulo
    }
}
