using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShild : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f;
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    public GameObject projectilePrefab; // Use um prefab para criar projéteis

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        Caminhando();
        CheckAttack(); // Chama uma função separada para verificar o ataque
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

    private void CheckAttack()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, followDistance, LayerMask.GetMask("Player"));
        if (col != null && col.CompareTag("Player"))
        {
            Atacando(col);
        }
    }

    public void Atacando(Collider2D col)
    {
        // Evita iniciar múltiplas corrotinas
        if (Time.time > lastAttackTime + attackCooldown)
        {
            StartCoroutine(ChegandoPerto(col));
        }
    }

    private IEnumerator ChegandoPerto(Collider2D col)
    {
        // Mantém a referência da distância do jogador
        float distancePlayer = Vector2.Distance(transform.position, player.position);

        // Enquanto o jogador estiver próximo, aguarda
        while (distancePlayer <= followDistance)
        {
            // Atualiza a distância
            distancePlayer = Vector2.Distance(transform.position, player.position);

            // Verifica se a colisão é com uma bala do jogador
            if (col.gameObject.CompareTag("BalasPlayer"))
            {
                Destroy(col.gameObject); // Destroi a instância da bala
                yield break; // Sai do loop
            }

            yield return null; // Espera até o próximo frame
        }

        // Se ainda estiver dentro da distância e for o jogador
        if (col.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            lastAttackTime = Time.time; // Atualiza o tempo do último ataque
            Debug.Log("Atacando");
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("CoolDawn Iniciado");
        }
    }
}
