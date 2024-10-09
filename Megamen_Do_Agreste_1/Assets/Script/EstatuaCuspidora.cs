using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatuaCuspidora : MonoBehaviour
{
    public GameObject projectilePrefab; // O prefab do projétil
    public Transform firePoint; // O ponto de onde o projétil será disparado
    public float fireRate = 1f; // Taxa de disparo (dispara a cada segundo)
    private float nextFireTime = 0f; // Tempo para o próximo disparo

    private GameObject target; // Referência ao objeto alvo

    void Start()
    {
        // Encontra o objeto alvo na cena (substitua "Circle" pelo nome da tag que você definiu)
        target = GameObject.FindGameObjectWithTag("Circle");
    }

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
        // Cria o projétil na posição do ponto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Adiciona um componente ProjectileMovement ao projétil para movê-lo em direção ao alvo
        ProjectileMovement movement = projectile.AddComponent<ProjectileMovement>();
        movement.target = target.transform; // Define o alvo
        movement.speed = 5f; // Ajuste a velocidade conforme necessário
    }
}

// Script para controlar o movimento do projétil
public class ProjectileMovement : MonoBehaviour
{
    public Transform target; // Alvo que o projétil deve seguir
    public float speed = 5f; // Velocidade do projétil

    void Update()
    {
        // Verifica se há um alvo definido
        if (target != null)
        {
            // Move o projétil em direção ao alvo
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Verifica se o projétil atingiu o alvo
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(gameObject); // Destrói o projétil ao atingir o alvo
            }
        }
        else
        {
            // Destrói o projétil se não houver alvo
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com o objeto alvo
        if (collision.gameObject.CompareTag("Circle")) // Substitua "Circle" pela tag do seu objeto
        {
            Destroy(gameObject); // Destrói o projétil ao colidir com o alvo
        }
    }
}
