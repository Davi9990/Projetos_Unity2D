using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySamurai : MonoBehaviour
{
    public Transform player;           // Referência ao transform do jogador
    public float detectionRange = 15f; // Distância em que o inimigo detecta o jogador
    public float dashSpeed = 10f;      // Velocidade do dash
    public float dashDuration = 0.2f;  // Duração do dash em segundos
    public float dashCooldown = 1f;    // Tempo de recarga entre dashes em segundos
    public int damage = 20;            // Dano causado ao jogador
    public string playerTag = "Player"; // Tag usada para identificar o jogador

    private bool isDashing = false;
    private float dashTime = 0f;
    private float lastDashTime = 0f;

    void Update()
    {
        // Verifica se o jogador está dentro da distância de detecção
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && Time.time > lastDashTime + dashCooldown)
        {
            StartCoroutine(DashTowardsPlayer());
        }
    }

    private IEnumerator DashTowardsPlayer()
    {
        isDashing = true;
        dashTime = 0f;
        lastDashTime = Time.time;

        Vector2 direction = (player.position - transform.position).normalized;

        while (dashTime < dashDuration)
        {
            transform.position += (Vector3)(direction * dashSpeed * Time.deltaTime);
            dashTime += Time.deltaTime;
            yield return null;
        }

        // Verifica se o inimigo está perto o suficiente do jogador para causar dano
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, 1f); // Ajuste essa distância conforme necessário
        foreach (Collider2D hitPlayer in hitPlayers)
        {
            if (hitPlayer.CompareTag(playerTag))
            {
                Barra_de_Vida playerHealth = hitPlayer.GetComponent<Barra_de_Vida>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }

        isDashing = false;
    }

    // Método para desenhar a área de detecção no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f); // Ajuste o raio conforme necessário
    }
}
