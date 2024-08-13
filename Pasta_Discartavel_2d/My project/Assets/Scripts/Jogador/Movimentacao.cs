using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private Rigidbody2D rbPlayer; // Refer�ncia ao componente Rigidbody2D do jogador
    private Animator animator; // Refer�ncia ao componente Animator do jogador

    [SerializeField] private float speed = 5f; // Velocidade de movimento do jogador
    [SerializeField] private float jumpForce = 15f; // For�a do pulo do jogador
    [SerializeField] private Transform groundCheck; // Ponto de refer�ncia para verificar se o jogador est� no ch�o
    [SerializeField] private LayerMask groundLayer; // M�scara de camada para identificar o que � considerado ch�o

    private bool isJumping; // Verifica se o jogador est� pulando
    private bool isGrounded; // Verifica se o jogador est� no ch�o
    private bool jumpRequest; // Indica se foi feita uma solicita��o de pulo

    private static Movimentacao instance; // Inst�ncia �nica da classe para implementar o padr�o Singleton

    private void Awake()
    {
        // Implementar Singleton para garantir uma �nica inst�ncia da classe Movimentacao
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroi a inst�ncia duplicada, mantendo apenas uma
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // N�o destr�i o objeto ao carregar uma nova cena

        rbPlayer = GetComponent<Rigidbody2D>(); // Obt�m o componente Rigidbody2D anexado ao jogador
        animator = GetComponent<Animator>(); // Obt�m o componente Animator anexado ao jogador
    }

    private void Update()
    {
        // Verifica se o jogador est� no ch�o usando um Linecast entre a posi��o do jogador e o ponto de groundCheck
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);

        // Atualiza o par�metro "IsGrounded" no Animator para refletir se o jogador est� no ch�o
        animator.SetBool("IsGrounded", isGrounded);
        // Atualiza o par�metro "Speed" no Animator para refletir a velocidade horizontal do jogador
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));

        // Verifica se o jogador pressionou o bot�o de pulo e se ele est� no ch�o
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true; // Solicita um pulo
        }

        // Se o bot�o de pulo for solto e o jogador estiver subindo, reduz a velocidade de subida
        if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y > 0)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f); // Reduz a velocidade vertical em 50%
        }
    }

    private void FixedUpdate()
    {
        Move(); // Chama o m�todo de movimenta��o

        // Se h� uma solicita��o de pulo e o jogador est� no ch�o, realiza o pulo
        if (jumpRequest && isGrounded)
        {
            JumpPlayer(); // Chama o m�todo para pular
            jumpRequest = false; // Reseta a solicita��o de pulo
        }

        // Atualiza o par�metro "IsJumping" no Animator para refletir se o jogador est� no ar
        animator.SetBool("IsJumping", !isGrounded);
    }

    // M�todo para controlar a movimenta��o horizontal do jogador
    void Move()
    {
        float xMove = Input.GetAxis("Horizontal"); // Obt�m o input horizontal (esquerda/direita)
        rbPlayer.velocity = new Vector2(xMove * speed, rbPlayer.velocity.y); // Define a velocidade do jogador

        // Ajusta a rota��o do jogador com base na dire��o do movimento
        if (xMove > 0)
        {
            transform.eulerAngles = new Vector2(0, 0); // Se estiver movendo para a direita, mant�m a rota��o normal
        }
        else if (xMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180); // Se estiver movendo para a esquerda, vira o jogador
        }
    }

    // M�todo para realizar o pulo do jogador
    void JumpPlayer()
    {
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpForce); // Aplica uma for�a vertical para o pulo
    }
}
