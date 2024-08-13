using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBossIA : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade de movimento horizontal
    public float jumpForce = 10f;  // Força do pulo
    public float jumpInterval = 2f;  // Intervalo entre pulos
    public float fireRate = 3f;  // Intervalo entre disparos de fogo
    public GameObject firePrefab;  // Prefab do projétil de fogo
    public Transform fireSpawnPoint;  // Ponto de origem para o projétil de fogo

    private Rigidbody2D rb;  // Referência ao Rigidbody2D do objeto
    private bool isGrounded = true;  // Verifica se o objeto está no chão
    private float jumpTimer;  // Cronômetro para controlar o intervalo de pulo
    private float fireTimer;  // Cronômetro para controlar o intervalo de disparo de fogo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtém a referência ao Rigidbody2D do objeto
        jumpTimer = jumpInterval;  // Inicializa o cronômetro de pulo
        fireTimer = fireRate;  // Inicializa o cronômetro de disparo de fogo
    }

    void Update()
    {
        // Movimentação horizontal com um padrão de oscilação (PingPong) entre -1 e 1
        float moveDirection = Mathf.PingPong(Time.time * moveSpeed, 2) - 1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Controle de pulo
        jumpTimer -= Time.deltaTime;  // Atualiza o cronômetro de pulo
        if (jumpTimer <= 0 && isGrounded)  // Se o cronômetro chegou a 0 e o objeto está no chão
        {
            Jump();  // Executa o pulo
            jumpTimer = jumpInterval;  // Reinicia o cronômetro de pulo
        }

        // Controle de tiro de fogo
        fireTimer -= Time.deltaTime;  // Atualiza o cronômetro de disparo de fogo
        if (fireTimer <= 0)  // Se o cronômetro chegou a 0
        {
            ShootFire();  // Executa o disparo de fogo
            fireTimer = fireRate;  // Reinicia o cronômetro de disparo de fogo
        }
    }

    private void Jump()
    {
        // Aplica uma força vertical ao Rigidbody2D para fazer o pulo
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ShootFire()
    {
        // Instancia um novo projétil de fogo na posição de origem especificada
        GameObject fireball = Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity);
        // O movimento do projétil de fogo será controlado por um script separado
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Marca o objeto como estando no chão quando colide com um objeto com a tag "Ground"
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Lógica para quando o boss colidir com o jogador (não implementada aqui)
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Marca o objeto como não estando no chão quando sai de um objeto com a tag "Ground"
            isGrounded = false;
        }
    }
}
