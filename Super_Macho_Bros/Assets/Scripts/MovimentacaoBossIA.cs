using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBossIA : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public float fireballCooldown = 3f;
    public float detectionRange = 10f;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastFireballTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastFireballTime = Time.time;
    }

    private void Update()
    {
        if (IsPlayerInRange())
        {
            if (Time.time > lastFireballTime + fireballCooldown)
            {
                SpitFire();
                lastFireballTime = Time.time;
            }
        }
        else
        {
            RandomMovement();
        }
    }

    private bool IsPlayerInRange()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        return player != null;
    }

    private void SpitFire()
    {
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }

    private void RandomMovement()
    {
        int randomAction = Random.Range(0, 3);

        switch (randomAction)
        {
            case 0:
                MoveForward();
                break;
            case 1:
                MoveBackward();
                break;
            case 2:
                if (isGrounded)
                {
                    Jump();
                }
                break;
        }
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        transform.localScale = new Vector3(1, 1, 1); // Ajuste a direção do sprite
    }

    private void MoveBackward()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        transform.localScale = new Vector3(-1, 1, 1); // Ajuste a direção do sprite
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
