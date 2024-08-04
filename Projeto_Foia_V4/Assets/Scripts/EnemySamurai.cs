using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySamurai : MonoBehaviour
{
    public Transform player;           // Refer�ncia ao transform do jogador
    public float detectionRange = 15f; // Dist�ncia em que o inimigo detecta o jogador
    public float dashSpeed = 10f;      // Velocidade do dash
    public float dashDuration = 0.2f;  // Dura��o do dash em segundos
    public float dashCooldown = 1f;    // Tempo de recarga entre dashes em segundos
    public int damage = 20;            // Dano causado ao jogador
    public string playerTag = "Player"; // Tag usada para identificar o jogador

    private bool isDashing = false;
    private float dashTime = 0f;
    private float lastDashTime = 0f;

    void Update()
    {
        // Verifica se o jogador est� dentro da dist�ncia de detec��o
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

        // Verifica se o inimigo est� perto o suficiente do jogador para causar dano
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, 1f); // Ajuste essa dist�ncia conforme necess�rio
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

    // M�todo para desenhar a �rea de detec��o no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f); // Ajuste o raio conforme necess�rio
    }
}
