using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarantula : MonoBehaviour
{
    public string playerTag = "Player"; // Tag do jogador
    public float Distancia = 10f; // Distância em que o inimigo começa a seguir o jogador
    public float velocidade = 10f; // Velocidade do dash
    public float dashduration = 0.2f; // Duração do dash
    public float dashCooldown = 1f; // Tempo de recarga do dash
    public float recoilDistance = 1f; // Distância para recuar após o dash
    private SpriteRenderer render; // Referência do sprite para o flip
    public bool isDashing = false;
    private Animator anim;

    private Rigidbody2D EnemyRb;
    private float lastDashTime = 0f;

    void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        EnemyRb.gravityScale = 0; // Mantém no teto até o player se aproximar
        render = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag); // Procura o jogador pelo tag

        if (player != null) // Apenas executa a lógica se o jogador existir
        {
            Descendo(player.transform);
            Perseguindo(player.transform);
        }
    }

    public void Descendo(Transform playerTransform)
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= Distancia)
        {
            EnemyRb.gravityScale = 1; // Começa a cair quando o player se aproxima
            FlipVertically(); // Adiciona o flip vertical
        }
    }

    public void Perseguindo(Transform playerTransform)
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= Distancia && !isDashing && Time.time > lastDashTime + dashCooldown)
        {
            StartCoroutine(DashTowardsPlayer(playerTransform));
        }
    }

    private IEnumerator DashTowardsPlayer(Transform playerTransform)
    {
        isDashing = true;
        lastDashTime = Time.time;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 dashVelocity = direction * velocidade;

        float dashStartTime = Time.time;

        // Aplica a velocidade do dash por um curto período
        while (Time.time < dashStartTime + dashduration)
        {
            EnemyRb.velocity = dashVelocity; // Aplica a velocidade do dash
            anim.SetBool("Perseguindo", true);
            yield return null;
        }

        // Para o movimento após o dash
        EnemyRb.velocity = Vector2.zero;
        anim.SetBool("Perseguindo", false);

        // Recuar um pouco após o dash
        Vector2 recoilDirection = -direction * recoilDistance;
        EnemyRb.position += recoilDirection; // Move o inimigo para trás

        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a Tarântula colidiu com o jogador
        if (collision.gameObject.CompareTag(playerTag) && isDashing)
        {
            // Para o movimento imediatamente e recua
            EnemyRb.velocity = Vector2.zero; // Para o movimento
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 recoilDirection = -direction * recoilDistance;
            EnemyRb.position += recoilDirection; // Move o inimigo para trás
            isDashing = false; // Reseta o estado de dashing
        }
    }

    private void FlipVertically()
    {
        // Inverte o sprite verticalmente ao descer
        if (!render.flipY) // Verifica se já está invertido
        {
            render.flipY = true; // Inverte o sprite
        }
    }
}
