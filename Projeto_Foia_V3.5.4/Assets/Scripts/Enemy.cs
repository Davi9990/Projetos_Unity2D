using System;
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

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        }
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
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        rb.isKinematic = true; // Torna o Rigidbody2D do inimigo cinemático ao colidir com o jogador
    //    }
    //}

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

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        rb.isKinematic = false; // Volta ao estado normal quando a colisão termina
    //    }
    //}
}
