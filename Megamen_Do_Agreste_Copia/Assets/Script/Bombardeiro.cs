using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardeiro : MonoBehaviour
{
    public float shootingRange = 15f; //Distancia em que o inimigo começa a atirar no jogador
    public float fleeingRange = 5f; //Distencia em que o inimigo começa a fugir do jogador
    public float moveSpeed = 2f; //Velocidade de movimento do inimigo
    public float shootingCooldown = 1f; //Tempo de recarga entre disparos
    public GameObject projectilePrefab; //Prefab do projetil
    public Transform shootingPoint1; //Ponto de onde o projetil é disparados
    public Transform shootingPoint2;
    public float projectileSpeed = 10f; //Velocidade do projetil
    public float projectileLifeTime = 5f; //Tempo de Vida do Projetil em segundos

    private float lastShotTime;
    private Transform player;

    void Start()
    {
        //Encontra o jogador pela "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        //Calcula a distancia entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //Se oo jogador estiver dentro do raio de fuga, o inimigo foge
        if (distanceToPlayer <= fleeingRange)
        {
            FleeFromPlayer();
        }
        //Se o inimigo estiver dentro do raio de fuga, o inmigo foge
        else if (distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
        }
    }

    void FleeFromPlayer()
    {
        //Move o inimigo na direção oposta ao jogador
        Vector2 direction = (transform.position - player.transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void ShootAtPlayer()
    {
        if (Time.time > lastShotTime + shootingCooldown)
        {
            // Cria os projéteis nas posições das mãos
            GameObject projectile1 = Instantiate(projectilePrefab, shootingPoint1.position, Quaternion.identity);
            GameObject projectile2 = Instantiate(projectilePrefab, shootingPoint2.position, Quaternion.identity);

            // Calcula a direção horizontal em relação ao jogador
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 launchVelocity = new Vector2(direction.x * projectileSpeed, projectileSpeed); // Adiciona componente vertical

            Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();

            if (rb1 != null && rb2 != null)
            {
                // Define a gravidade e aplica a velocidade de lançamento para criar a parábola
                rb1.gravityScale = 1; // Permite que a gravidade afete o projétil
                rb1.velocity = launchVelocity;

                rb2.gravityScale = 1;
                rb2.velocity = launchVelocity;
            }

            // Destrói os projéteis após o tempo de vida
            Destroy(projectile1, projectileLifeTime);
            Destroy(projectile2, projectileLifeTime);

            // Atualiza o tempo do último disparo
            lastShotTime = Time.time;
        }
    }
}
