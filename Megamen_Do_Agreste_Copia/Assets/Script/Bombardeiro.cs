using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardeiro : Todos
{
    public float shootingRange = 15f; // Dist�ncia em que o inimigo come�a a atirar no jogador
    public float fleeingRange = 5f; // Dist�ncia em que o inimigo come�a a fugir do jogador
    public float shootingCooldown = 1f; // Tempo de recarga entre disparos
    public GameObject projectilePrefab; // Prefab do proj�til
    public Transform shootingPoint1; // Ponto de onde o proj�til � disparado
    public Transform shootingPoint2;
    public float projectileSpeed = 10f; // Velocidade do proj�til
    public float projectileLifeTime = 5f; // Tempo de vida do proj�til em segundos

    private float lastShotTime;
    private Transform player;

    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Encontra o jogador pela "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // Verifica se o jogador foi destru�do, e se for o caso, encontra o novo jogador
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            return; // Sai do Update caso o jogador ainda n�o tenha sido encontrado
        }

        // Calcula a dist�ncia entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Se o jogador estiver dentro do raio de fuga, o inimigo foge
        if (distanceToPlayer <= fleeingRange)
        {
            FleeFromPlayer();
            anim.SetBool("Atirando", true);
        }
        // Se o inimigo estiver dentro do raio de tiro, o inimigo atira
        else if (distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
        }
        else if (distanceToPlayer >= fleeingRange)
        {
            anim.SetBool("Atirando", false);
        }
    }

    void FleeFromPlayer()
    {
        // Move o inimigo na dire��o oposta ao jogador
        Vector2 direction = (transform.position - player.transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void ShootAtPlayer()
    {
        if (Time.time > lastShotTime + shootingCooldown)
        {
            // Cria os proj�teis nas posi��es das m�os
            GameObject projectile1 = Instantiate(projectilePrefab, shootingPoint1.position, Quaternion.identity);
            GameObject projectile2 = Instantiate(projectilePrefab, shootingPoint2.position, Quaternion.identity);

            // Calcula a dire��o horizontal em rela��o ao jogador
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 launchVelocity = new Vector2(direction.x * projectileSpeed, projectileSpeed); // Adiciona componente vertical

            Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();

            if (rb1 != null && rb2 != null)
            {
                // Define a gravidade e aplica a velocidade de lan�amento para criar a par�bola
                rb1.gravityScale = 1; // Permite que a gravidade afete o proj�til
                rb1.velocity = launchVelocity;

                rb2.gravityScale = 1;
                rb2.velocity = launchVelocity;
            }

            // Destr�i os proj�teis ap�s o tempo de vida
            Destroy(projectile1, projectileLifeTime);
            Destroy(projectile2, projectileLifeTime);

            // Atualiza o tempo do �ltimo disparo
            lastShotTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
