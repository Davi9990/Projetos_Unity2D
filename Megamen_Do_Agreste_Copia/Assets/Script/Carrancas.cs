using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrancas : MonoBehaviour
{
    public GameObject projectilePrefab; // O prefab do proj�til
    public Transform firePointLeft; // Ponto de disparo para a esquerda
    public Transform firePointRight; // Ponto de disparo para a direita
    public float fireRate = 1f; // Taxa de disparo (dispara a cada segundo)
    public float projectileLifetime = 5f; // Tempo de vida dos proj�teis
    public float projectileSpeed = 5f; // Velocidade dos proj�teis

    private float nextFireTime = 0f; // Tempo para o pr�ximo disparo

    void Update()
    {
        // Verifica se � hora de atirar
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Atualiza o tempo do pr�ximo disparo
        }
    }

    void Shoot()
    {
        // Dispara para a esquerda
        GameObject projectileLeft = Instantiate(projectilePrefab, firePointLeft.position, firePointLeft.rotation);
        Rigidbody2D rbLeft = projectileLeft.GetComponent<Rigidbody2D>();
        rbLeft.velocity = Vector2.left * projectileSpeed; // Proj�til vai para a esquerda

        // Dispara para a direita
        GameObject projectileRight = Instantiate(projectilePrefab, firePointRight.position, firePointRight.rotation);
        Rigidbody2D rbRight = projectileRight.GetComponent<Rigidbody2D>();
        rbRight.velocity = Vector2.right * projectileSpeed; // Proj�til vai para a direita

        // Destr�i os proj�teis ap�s um tempo
        Destroy(projectileLeft, projectileLifetime);
        Destroy(projectileRight, projectileLifetime);
    }
}
