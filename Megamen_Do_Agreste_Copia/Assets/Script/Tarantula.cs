using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarantula : MonoBehaviour
{
    public Transform player; // Refer�ncia ao transform do Jogador
    public float Distancia = 10f; // Dist�ncia em que o inimigo come�a a seguir o jogador
    public float velocidade = 10f; // Velocidade do dash
    public float dashduration = 0.2f; // Dura��o do dash
    public float dashCooldown = 1f; // Tempo de recarga do dash
    public float recoilDistance = 1f; // Dist�ncia para recuar ap�s o dash
    private SpriteRenderer render; // Refer�ncia do sprite para o flip
    public bool isDashing = false;
    private Animator anim;

    private Rigidbody2D EnemyRb;
    private float lastDashTime = 0f;

    void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        EnemyRb.gravityScale = 0; // Mant�m no teto at� o player se aproximar
        render = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Descendo();
        Perseguindo();
    }

    public void Descendo()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= Distancia)
        {
            EnemyRb.gravityScale = 1; // Come�a a cair quando o player se aproxima
            FlipVertically(); // Adiciona o flip vertical
        }
    }

    public void Perseguindo()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= Distancia && !isDashing && Time.time > lastDashTime + dashCooldown)
        {
            StartCoroutine(DashTowardsPlayer());
        }
    }

    private IEnumerator DashTowardsPlayer()
    {
        isDashing = true;
        lastDashTime = Time.time;

        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 dashVelocity = direction * velocidade;

        float dashStartTime = Time.time;

        // Aplica a velocidade do dash por um curto per�odo
        while (Time.time < dashStartTime + dashduration)
        {
            EnemyRb.velocity = dashVelocity; // Aplica a velocidade do dash
            anim.SetBool("Perseguindo", true);
            yield return null;
        }

        // Para o movimento ap�s o dash
        EnemyRb.velocity = Vector2.zero;
        anim.SetBool("Perseguindo",false);

        // Recuar um pouco ap�s o dash
        Vector2 recoilDirection = -direction * recoilDistance;
        EnemyRb.position += recoilDirection; // Move o inimigo para tr�s

        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a Tar�ntula colidiu com o jogador
        if (collision.gameObject.CompareTag("Player") && isDashing)
        {
            // Para o movimento imediatamente e recua
            EnemyRb.velocity = Vector2.zero; // Para o movimento
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 recoilDirection = -direction * recoilDistance;
            EnemyRb.position += recoilDirection; // Move o inimigo para tr�s
            isDashing = false; // Reseta o estado de dashing
        }
    }

    private void FlipVertically()
    {
        // Inverte o sprite verticalmente ao descer
        if (!render.flipY) // Verifica se j� est� invertido
        {
            render.flipY = true; // Inverte o sprite
        }
    }
}
