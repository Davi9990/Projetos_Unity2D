using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;        // Referência ao transform do jogador
    public float followDistance = 10f; // Distância em que o inimigo começa a seguir o jogador
    public float moveSpeed = 2f;    // Velocidade de movimento do inimigo
    public int damage = 10;         // Dano que o inimigo causa ao jogador
    public float attackCooldown = 1f; // Tempo de recarga entre ataques
    private float lastAttackTime;
    private float originalSpeed;    // Para armazenar a velocidade original

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer para o flip
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no inimigo!");
        }

        originalSpeed = moveSpeed; // Armazena a velocidade original
    }

    void Update()
    {
        // Calcula a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Se a distância for menor ou igual ao followDistance, o inimigo segue o jogador
        if (distanceToPlayer <= followDistance)
        {
            // Move o inimigo na direção do jogador
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            animator.SetBool("Chasing", true);

            // Ajusta o flip do sprite baseado na direção do movimento
            if (direction.x > 0 && spriteRenderer.flipX)
            {
                Flip();
            }
            else if (direction.x < 0 && !spriteRenderer.flipX)
            {
                Flip();
            }
        }
        else
        {
            animator.SetBool("Chasing", false);
        }
    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void ReduceSpeed()
    {
        StartCoroutine(ReduceSpeedCoroutine());
    }

    IEnumerator ReduceSpeedCoroutine()
    {
        moveSpeed /= 2; // Reduz a velocidade pela metade
        yield return new WaitForSeconds(1f); // Espera 1 segundo
        moveSpeed = originalSpeed; // Restaura a velocidade original
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
        {
            Barra_de_Vida Vida = collision.gameObject.GetComponent<Barra_de_Vida>();
            if (Vida != null)
            {
                Vida.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}
