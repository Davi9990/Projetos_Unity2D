using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool movingRight;
    private bool hasStopped = false;
    private StackManeger stackManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Decide a direção com base na posição inicial
        movingRight = transform.position.x < 0;

        // Movimento inicial
        float dir = movingRight ? 1 : -1;
        rb.velocity = new Vector2(dir * moveSpeed, 0f);

        // Procura o StackManager na cena
        stackManager = FindObjectOfType<StackManeger>();
    }

    void Update()
    {
        if (!hasStopped)
        {
            float moveDir = movingRight ? 1 : -1;
            rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Se colidir com o chão ou outro bloco, parar o movimento e notificar o StackManager
        if (!hasStopped && (other.collider.CompareTag("Chao") || other.collider.CompareTag("Player")))
        {
            hasStopped = true;

            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;

            if (stackManager != null)
            {
                stackManager.MoveSpawnPointsUp();
            }
        }
    }
}
