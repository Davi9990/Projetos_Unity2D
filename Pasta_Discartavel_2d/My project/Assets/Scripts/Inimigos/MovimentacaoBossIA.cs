using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBossIA : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade de movimento horizontal
    public float jumpForce = 10f;  // For�a do pulo
    public float jumpInterval = 2f;  // Intervalo entre pulos
    public float fireRate = 3f;  // Intervalo entre disparos de fogo
    public GameObject firePrefab;  // Prefab do proj�til de fogo
    public Transform fireSpawnPoint;  // Ponto de origem para o proj�til de fogo

    private Rigidbody2D rb;  // Refer�ncia ao Rigidbody2D do objeto
    private bool isGrounded = true;  // Verifica se o objeto est� no ch�o
    private float jumpTimer;  // Cron�metro para controlar o intervalo de pulo
    private float fireTimer;  // Cron�metro para controlar o intervalo de disparo de fogo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obt�m a refer�ncia ao Rigidbody2D do objeto
        jumpTimer = jumpInterval;  // Inicializa o cron�metro de pulo
        fireTimer = fireRate;  // Inicializa o cron�metro de disparo de fogo
    }

    void Update()
    {
        // Movimenta��o horizontal com um padr�o de oscila��o (PingPong) entre -1 e 1
        float moveDirection = Mathf.PingPong(Time.time * moveSpeed, 2) - 1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Controle de pulo
        jumpTimer -= Time.deltaTime;  // Atualiza o cron�metro de pulo
        if (jumpTimer <= 0 && isGrounded)  // Se o cron�metro chegou a 0 e o objeto est� no ch�o
        {
            Jump();  // Executa o pulo
            jumpTimer = jumpInterval;  // Reinicia o cron�metro de pulo
        }

        // Controle de tiro de fogo
        fireTimer -= Time.deltaTime;  // Atualiza o cron�metro de disparo de fogo
        if (fireTimer <= 0)  // Se o cron�metro chegou a 0
        {
            ShootFire();  // Executa o disparo de fogo
            fireTimer = fireRate;  // Reinicia o cron�metro de disparo de fogo
        }
    }

    private void Jump()
    {
        // Aplica uma for�a vertical ao Rigidbody2D para fazer o pulo
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ShootFire()
    {
        // Instancia um novo proj�til de fogo na posi��o de origem especificada
        GameObject fireball = Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity);
        // O movimento do proj�til de fogo ser� controlado por um script separado
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Marca o objeto como estando no ch�o quando colide com um objeto com a tag "Ground"
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // L�gica para quando o boss colidir com o jogador (n�o implementada aqui)
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Marca o objeto como n�o estando no ch�o quando sai de um objeto com a tag "Ground"
            isGrounded = false;
        }
    }
}
