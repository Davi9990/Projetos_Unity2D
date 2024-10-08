using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShild : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f;
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    public float delayBeforeAttack = 1f;
    public float waitingTimeAfterAttack = 2f;
    private float lastAttackTime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        // Só movimenta o inimigo se ele não estiver atacando
        if (Time.time > lastAttackTime + attackCooldown + delayBeforeAttack + waitingTimeAfterAttack)
        {
            Caminhando();
        }
    }

    public void Caminhando()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.position);

        if (distancePlayer < followDistance)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 6;

            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BalasPlayer"))
        {
            float distancePlayer = Vector2.Distance(transform.position, player.position);
            if (distancePlayer < followDistance)
            {
                // Quando estiver perto do jogador, bala não destrói o inimigo
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
        {
            StartCoroutine(AtacarJogador());
        }
    }

    private IEnumerator AtacarJogador()
    {
        // Para o inimigo completamente antes de atacar
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        // Espera antes de atacar
        yield return new WaitForSeconds(delayBeforeAttack);


        // Realiza o ataque
        Debug.Log("Atacando o jogador");
        lastAttackTime = Time.time;

        // Espera um tempo antes de voltar a perseguir o jogador
        yield return new WaitForSeconds(waitingTimeAfterAttack);

        // Retorna ao estado normal, continuando a perseguir o jogador
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
