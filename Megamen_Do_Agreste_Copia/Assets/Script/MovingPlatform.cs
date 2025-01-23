using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Transform do Ponto A
    public Transform pointB; // Transform do Ponto B
    public float speed = 2f; // Velocidade da plataforma

    private Vector3 target; // O próximo ponto-alvo

    void Start()
    {
        // Define o ponto inicial de destino como o Ponto A
        if (pointA != null)
        {
            target = pointA.position;
        }
    }

    void Update()
    {
        // Move a plataforma na direção do ponto-alvo
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Verifica se a plataforma chegou ao ponto-alvo
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Troca o destino entre Ponto A e Ponto B
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    // Visualiza os pontos A e B no editor
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);
        }
    }
}
