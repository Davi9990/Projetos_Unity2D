using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movimentação_Bandido : MonoBehaviour
{
    public Transform player;
    public Transform player_Grande;
    public Transform player_Giga;

    public Rigidbody2D playerRb;
    public Rigidbody2D playerRbGrande; // Referência ao Player Grande
    public Rigidbody2D playerRbGiga; // Referenccia ao Player Giga

    public float followDistance = 10f;
    public float moveSpeed = 2f;
    private Rigidbody2D rb;

    //Bandido Fase 1
    [SerializeField] Transform starPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float JumpForce;
    private Vector2 direction;
    private bool movingToEnd = true;

    //Bandido Fase 2
    public float Reaload = 1f;
    public GameObject prefabProjetil;
    public Transform ShootPoint;
    public float ProjetilVelocity = 10f;
    public float LifeProjectTime = 5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(playerRb == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if(playerObject != null)
            {
                playerRb = playerObject.GetComponent<Rigidbody2D>();
            }
        }

        //Define a direção inicial
        direction = (endPoint.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if(playerRb != null && playerRb.gameObject.activeInHierarchy)
        {
            Covarde();
        }
    }

    void Covarde()
    {
       //Movimenta o inimigo entre o startPoint e endPoint
       if(movingToEnd)
       {
           direction = (endPoint.position - transform.position).normalized;
       }
       else
       {
           direction = (starPoint.position - transform.position).normalized;
       }

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Verifica se o inimigo chegou ao destino
        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f && movingToEnd)
        {
            movingToEnd = false; // Começa a voltar para o ponto inicial
        }
        else if (Vector2.Distance(transform.position, starPoint.position) < 0.1f && !movingToEnd)
        {
            movingToEnd = true; // Começa a ir para o endPoint novamente
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PontoDePulo"))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EndPoint") || collision.gameObject.CompareTag("Player") 
            || collision.gameObject.CompareTag("Player_Grande"))
        {
            transform.position = starPoint.position;
            rb.velocity = Vector2.zero;
            movingToEnd = true;
        }
    }
}
