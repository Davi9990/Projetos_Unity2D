using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardeiro : MonoBehaviour
{
    public float shootingRange = 15f;   // Dist�ncia em que o inimigo come�a a atirar no jogador
    public float fleeingRange = 5f;     // Dist�ncia em que o inimigo come�a a fugir do jogador
    public float moveSpeed = 2f;        // Velocidade de movimento do inimigo
    public float shootingCooldown = 1f; // Tempo de recarga entre disparos
    public GameObject projectilePrefab; // Prefab do projetil
    public Transform shootingPoint;     // Ponto de onde o projetil � disparado
    public float projectileSpeed = 10f; // Velocidade do projetil
    public float projectileLifetime = 5f; // Tempo de vida do projetil em segundos

    private float lastShotTime;
    private Transform player;

    void Start()
    {
        // Encontra o jogador pela tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // Calcula a dist�ncia entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Se o jogador estiver dentro do raio de fuga, o inimigo foge
        if (distanceToPlayer <= fleeingRange)
        {
            FleeFromPlayer();
        }
        // Se o jogador estiver dentro do raio de vis�o, o inimigo atira
        else if (distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
        }
    }

    void FleeFromPlayer()
    {
        // Move o inimigo na dire��o oposta ao jogador
        Vector2 direction = (transform.position - player.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void ShootAtPlayer()
    {
        if (Time.time > lastShotTime + shootingCooldown)
        {
            // Cria o projetil na posi��o do ponto de tiro e na dire��o do jogador
            GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
            Vector2 direction = (player.position - shootingPoint.position).normalized;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1; // Ativa a gravidade do projetil
                rb.velocity = direction * projectileSpeed;
            }

            // Certifique-se de que o SpriteRenderer esteja vis�vel
            SpriteRenderer sr = projectile.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;
            }

            // Destr�i o projetil ap�s o tempo de vida
            Destroy(projectile, projectileLifetime);
            lastShotTime = Time.time;
        }
    }
}
