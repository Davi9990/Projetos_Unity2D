using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShild : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f;
    public float stopDistance = 1.5f;  // Distância mínima para parar antes do ataque
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    public float delayBeforeAttack = 1f;
    public float waitingTimeAfterAttack = 2f;
    private float lastAttackTime;

    private Rigidbody2D rb;
    private bool isAttacking = false;  // Controla se o inimigo está no estado de ataque
    public bool isApproaching = true; // Controla se o inimigo está na fase de aproximação

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 6;  // Mantém a gravidade como dinâmica desde o início
    }

    void Update()
    {
        // Controla o movimento enquanto não está atacando
        if (!isAttacking && Time.time > lastAttackTime + attackCooldown + waitingTimeAfterAttack)
        {
            Caminhando();
        }
    }

    public void Caminhando()
    {
        float distancePlayer = Vector2.Distance(transform.position, player.position);

        if (distancePlayer < followDistance && distancePlayer > stopDistance)
        {
            isApproaching = true;  // O inimigo está se aproximando do jogador
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else if (distancePlayer <= stopDistance)
        {
            isApproaching = false;  // O inimigo está pronto para atacar
            rb.velocity = Vector2.zero;
        }
        else
        {
            isApproaching = false;  // O inimigo está fora do alcance
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BalasPlayer"))
        {
            // O inimigo só pode tomar dano se não estiver se aproximando do jogador
            if (!isApproaching)
            {
                // Obtém a referência ao script de vida do inimigo
                VidaShield vidaInimigo = GetComponent<VidaShield>();

                if (vidaInimigo != null)
                {
                    // Aplica dano ao inimigo
                    vidaInimigo.TakeDamege(1); // Substitua '1' pela quantidade de dano adequada
                }
            }
            else
            {
                // Bala é destruída, mas o inimigo permanece intacto
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Inimigo ataca se estiver colidindo com o jogador e fora do cooldown
        if (collision.collider.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown && !isAttacking)
        {
            StartCoroutine(AtacarJogador());
        }
    }

    private IEnumerator AtacarJogador()
    {
        isAttacking = true;  // Entra no estado de ataque
        rb.velocity = Vector2.zero;  // Para o movimento durante o ataque

        // Espera antes de atacar
        yield return new WaitForSeconds(delayBeforeAttack);

        // Realiza o ataque
        Debug.Log("Atacando o jogador");
        lastAttackTime = Time.time;

        // Espera após o ataque
        yield return new WaitForSeconds(waitingTimeAfterAttack);

        // Sai do estado de ataque e continua a perseguição
        isAttacking = false;
    }
}
