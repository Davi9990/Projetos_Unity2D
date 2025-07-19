using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoInelastica : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float n = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(n * speed, rb.velocity.y);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rbA = GetComponent<Rigidbody2D>();
        Rigidbody2D rbB = collision.rigidbody;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (rbA != null && rbB != null)
            {
                // Pega massas
                float m1 = rbA.mass;
                float m2 = rbB.mass;

                // Velocidades antes da colisão
                Vector2 v1 = rbA.velocity;
                Vector2 v2 = rbB.velocity;

                // Calcula velocidade final (conservação da quantidade de movimento)
                Vector2 vf = (m1 * v1 + m2 * v2) / (m1 + m2);

                // Aplica mesma velocidade para ambos (representa objetos grudando)
                rbA.velocity = vf;
                rbB.velocity = vf;

                // Opcional: Fixar a posição relativa entre os objetos
                rbB.transform.SetParent(rbA.transform);
            }
        }
    }
}
