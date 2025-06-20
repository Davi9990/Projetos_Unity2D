using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodaMove : MonoBehaviour
{
    [SerializeField] Transform startpoint;
    [SerializeField] Transform endpoint;
    [SerializeField] float speed = 5f;
    [SerializeField] float JumpForce;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startpoint.position;//Encaminha o objeto para o ponto inicial
        direction = (endpoint.position - transform.position).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveObjects();
    }

    void MoveObjects()
    {
        //Move o inimigo aplicando uma velocidade constante na direção do ponto final
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void TeleportToStart()
    {
        transform.position = startpoint.position;
        rb.velocity = Vector2.zero;//Reseta a velocidade paa evitar problemas
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EndPoint") || collision.gameObject.CompareTag("Player"))
        {
            TeleportToStart();
        }
    }
}
