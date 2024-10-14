using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabeçaVoadora : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speed = 5f;
    [SerializeField] float amplitude = 2f; // Altura do movimento serpenteante
    [SerializeField] float frequency = 2f;// Frequencia do movimento serpenteante
    [SerializeField] string ignoreCollisionTag; //Tag a ser ignorada

    private Rigidbody2D rb;
    private Vector2 direction;
    private float originalY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPoint.position;
        direction = (endPoint.position - startPoint.position).normalized;
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveObjects();
    }

    void MoveObjects()
    {
       //Movimento horizontal em direção ao ponto final
       float horizontalVelocity = direction.x * speed;

        //Movimento vertical com padrão sinusoidal
        float verticalOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        //Aplica a nove velocidade ao RigidBody
        rb.velocity = new Vector2(horizontalVelocity, verticalOffset);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se o inimigo colidiu com o ponto final
        if (collision.gameObject.CompareTag("EndPoint") || collision.gameObject.CompareTag("Player"))
        {
            TeleportToStart();
        }

        //Verifica se o inimigo colidiu com a tag a ser ignorada
        if (collision.gameObject.CompareTag(ignoreCollisionTag))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    public void TeleportToStart()
    {
        transform.position = startPoint.position;
        rb.velocity = Vector2.zero; //Reseta a velocidade para evitar problemas
        originalY = startPoint.position.y;//Reseta a posição Y ao teleporte
    }
}
