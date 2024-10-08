using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoInimigo : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speed = 5f;
    [SerializeField] float JumpForce;

    private Rigidbody2D rb;
    private Vector2 direction;

    public AudioSource src;

    public AudioClip pulo, surgir;

    private void Awake()
    {
        src.clip = surgir;
        src.Play();
    }

    void Start()
    {
        
        
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPoint.position;
        direction = (endPoint.position - startPoint.position).normalized;
    }

    void FixedUpdate()
    {
        MoveObjects();
    }

    void MoveObjects()
    {
        // Move o inimigo aplicando uma velocidade constante na direção do ponto final
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o inimigo colidiu com o ponto final
        if (collision.gameObject.CompareTag("EndPoint") || collision.gameObject.CompareTag("Player"))
        {
            TeleportToStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PontoDePulo"))
        {
            rb.velocity = Vector2.up * JumpForce;
            src.clip = pulo;
            src.Play();
        }
    }

    public void TeleportToStart()
    {
        transform.position = startPoint.position;
        rb.velocity = Vector2.zero; // Reseta a velocidade para evitar problemas
    }
}
