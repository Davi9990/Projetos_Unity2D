using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShild : MonoBehaviour
{
    public Transform player;
    public float followDistance = 10f;
    public float stopDistance = 1.5f;  // Dist�ncia m�nima para parar antes do ataque
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    public float delayBeforeAttack = 1f;
    public float waitingTimeAfterAttack = 2f;
    private float lastAttackTime;
    private Animator anim;

    private Rigidbody2D rb;
    private bool isAttacking = false;  // Controla se o inimigo est� no estado de ataque
    public bool isApproaching = true; // Controla se o inimigo est� na fase de aproxima��o
    private bool facingRight = true; // Controla a dire��o que o inimigo est� virado

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 6;  // Mant�m a gravidade como din�mica desde o in�cio
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Controla o movimento enquanto n�o est� atacando
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
            isApproaching = true;  // O inimigo est� se aproximando do jogador
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            anim.SetBool("Perseguindo",true);
            anim.SetBool("Atacando",false);

            // Verifica se precisa virar para a esquerda ou para a direita
            if (player.position.x > transform.position.x && facingRight) // Player � esquerda e inimigo virado � direita
            {
                Flip(); // Vira para a esquerda
            }
            else if (player.position.x < transform.position.x && !facingRight) // Player � direita e inimigo virado � esquerda
            {
                Flip(); // Vira para a direita
            }
        }
        else if (distancePlayer <= stopDistance)
        {
            isApproaching = false;  // O inimigo est� pronto para atacar
            rb.velocity = Vector2.zero;
            anim.SetBool("Perseguindo",false);
        }
        else
        {
            isApproaching = false;  // O inimigo est� fora do alcance
            rb.velocity = Vector2.zero;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // Inverte a escala no eixo X para fazer o flip
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BalasPlayer"))
        {
            // O inimigo s� pode tomar dano se n�o estiver se aproximando do jogador
            if (!isApproaching)
            {
                // Obt�m a refer�ncia ao script de vida do inimigo
                VidaShield vidaInimigo = GetComponent<VidaShield>();

                if (vidaInimigo != null)
                {
                    // Aplica dano ao inimigo
                    vidaInimigo.TakeDamege(1); // Substitua '1' pela quantidade de dano adequada
                }
            }
            else
            {
                // Bala � destru�da, mas o inimigo permanece intacto
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
        anim.SetBool("Atacando",true);
        lastAttackTime = Time.time;

        // Espera ap�s o ataque
        yield return new WaitForSeconds(waitingTimeAfterAttack);
        anim.SetBool("Atacando", false);
        anim.SetBool("Perseguindo",true);
        // Sai do estado de ataque e continua a persegui��o
        isAttacking = false;
    }
}
