using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabeçaVoadora : Todos
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float amplitude = 2f; // Altura do movimento serpenteante
    [SerializeField] float frequency = 2f; // Frequência do movimento serpenteante
    [SerializeField] string[] ignoreCollisionTags; // Tags a serem ignoradas

    private Vector2 direction;
    private float originalY;

    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        transform.position = startPoint.position;
        direction = (endPoint.position - startPoint.position).normalized;
        originalY = transform.position.y;
    }

    void FixedUpdate()
    {
        MoveObjects();
    }

    void MoveObjects()
    {
        // Movimento horizontal em direção ao ponto final
        float horizontalVelocity = direction.x * speed;

        // Movimento vertical com padrão sinusoidal
        float verticalOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Aplica a nova velocidade ao Rigidbody
        rb.velocity = new Vector2(horizontalVelocity, verticalOffset);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o inimigo colidiu com o ponto final ou jogador
        if (collision.gameObject.CompareTag("EndPoint") || collision.gameObject.CompareTag("Player"))
        {
            TeleportToStart();
        }

        // Verifica se a tag do objeto colidido está na lista de tags ignoradas
        if (System.Array.Exists(ignoreCollisionTags, tag => collision.gameObject.CompareTag(tag)))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    public void TeleportToStart()
    {
        transform.position = startPoint.position;
        rb.velocity = Vector2.zero; // Reseta a velocidade para evitar problemas
        originalY = startPoint.position.y; // Reseta a posição Y ao teleporte
    }
}
