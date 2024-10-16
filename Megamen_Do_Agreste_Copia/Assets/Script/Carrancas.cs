using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrancas : MonoBehaviour
{
    public GameObject projectilePrefab; // O prefab do projétil
    public Transform firePointLeft; // Ponto de disparo para a esquerda
    public Transform firePointRight; // Ponto de disparo para a direita
    public float fireRate = 1f; // Taxa de disparo (dispara a cada segundo)
    public float projectileLifetime = 5f; // Tempo de vida dos projéteis
    public float projectileSpeed = 5f; // Velocidade dos projéteis

    private float nextFireTime = 0f; // Tempo para o próximo disparo

    void Update()
    {
        // Verifica se é hora de atirar
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Atualiza o tempo do próximo disparo
        }
    }

    void Shoot()
    {
        // Dispara para a esquerda
        GameObject projectileLeft = Instantiate(projectilePrefab, firePointLeft.position, firePointLeft.rotation);
        Rigidbody2D rbLeft = projectileLeft.GetComponent<Rigidbody2D>();
        rbLeft.velocity = Vector2.left * projectileSpeed; // Projétil vai para a esquerda

        // Dispara para a direita
        GameObject projectileRight = Instantiate(projectilePrefab, firePointRight.position, firePointRight.rotation);
        Rigidbody2D rbRight = projectileRight.GetComponent<Rigidbody2D>();
        rbRight.velocity = Vector2.right * projectileSpeed; // Projétil vai para a direita

        // Destrói os projéteis após um tempo
        Destroy(projectileLeft, projectileLifetime);
        Destroy(projectileRight, projectileLifetime);
    }
}
