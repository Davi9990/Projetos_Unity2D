using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movimentação_Bandido : MonoBehaviour
{
    public Transform player;
    public Transform player_Grande;
    public Transform player_Giga;
    public float followDistance = 10f;
    public float moveSpeed = 2f;
    private Rigidbody2D rb;

    //Bandido Fase 1
    [SerializeField] Transform starPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float JumpForce;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = starPoint.position;
        direction = (endPoint.position - transform.position).normalized;

        if (rb == null)
        {
            Debug.Log("Rigidbody2D não encontrado no inimigo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if(distanceToPlayer <= followDistance)
        {
            Covarde();
        }
    }

    void Covarde()
    {
       
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PontoDePulo"))
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EndPoint") || collision.gameObject.CompareTag("Player"))
        {
            transform.position = starPoint.position;
            rb.velocity = Vector2.zero;
        }
    }
}
