using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatuaCuspidora : MonoBehaviour
{
    public GameObject projectilePrefab; // O prefab do proj�til
    public Transform firePoint; // O ponto de onde o proj�til ser� disparado
    public float fireRate = 1f; // Taxa de disparo (dispara a cada segundo)
    private float nextFireTime = 0f; // Tempo para o pr�ximo disparo

    [Header("Target Settings")]
    public string targetTag = "Circle"; // Tag do alvo (pode ser alterada no Inspector)

    private GameObject target; // Refer�ncia ao objeto alvo

    void Start()
    {
        // Encontra o objeto alvo na cena com a tag definida no Inspector
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

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
        // Cria o proj�til na posi��o do ponto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Adiciona um componente ProjectileMovement ao proj�til para mov�-lo em dire��o ao alvo
        ProjectileMovement movement = projectile.AddComponent<ProjectileMovement>();
        movement.target = target?.transform; // Define o alvo
        movement.speed = 5f; // Ajuste a velocidade conforme necess�rio
        movement.targetTag = targetTag; // Define a tag do alvo para o proj�til
    }
}

// Script para controlar o movimento do proj�til
public class ProjectileMovement : MonoBehaviour
{
    public Transform target; // Alvo que o proj�til deve seguir
    public float speed = 3f; // Velocidade do proj�til
    public string targetTag; // Tag do alvo para verifica��o de colis�o

    void Update()
    {
        // Verifica se h� um alvo definido
        if (target != null)
        {
            // Move o proj�til em dire��o ao alvo
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Verifica se o proj�til atingiu o alvo
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(gameObject); // Destr�i o proj�til ao atingir o alvo
            }
        }
        else
        {
            // Destr�i o proj�til se n�o houver alvo
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com o objeto alvo
        if (collision.gameObject.CompareTag(targetTag)) // Usando a tag definida no proj�til
        {
            Destroy(gameObject); // Destr�i o proj�til ao colidir com o alvo
        }
    }
}