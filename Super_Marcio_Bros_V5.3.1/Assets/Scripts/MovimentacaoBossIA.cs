using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBossIA : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpInterval = 2f;
    public float fireRate = 3f;
    public GameObject firePrefab;
    public Transform fireSpawnPoint;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float jumpTimer;
    private float fireTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpInterval;
        fireTimer = fireRate;
    }

    void Update()
    {
        // Movimentação horizontal
        float moveDirection = Mathf.PingPong(Time.time * moveSpeed, 2) - 1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Controle de pulo
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0 && isGrounded)
        {
            Jump();
            jumpTimer = jumpInterval;
        }

        // Controle de tiro de fogo
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            ShootFire();
            fireTimer = fireRate;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ShootFire()
    {
        GameObject fireball = Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity);
        // O script FireballMovement será responsável pela movimentação da bola de fogo
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Logic for when boss collides with player
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
