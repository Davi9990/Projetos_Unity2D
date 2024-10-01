using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    //Movimentação
    public float velocidade;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    //Pulos
    public int Pulos;
    public Transform VerificarChao;
    public bool EstaNoChao;
    public LayerMask chao;
    public float JumpForce;

    //Escadas
    private float vertical;
    private bool escada;
    private bool escalando;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        vertical = Input.GetAxis("Vertical");

        if (escada && Mathf.Abs(vertical) > 0)
        {
            escalando = true;
        }
    }

    private void Move()
    {
        //Captura o eixo horizontal
        float xAxis = Input.GetAxisRaw("Horizontal");

        //Atualiza a velocidade no eixo X, mantendo a velocidade Y existente
        rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);

        //Atualiza a direção do movimento e inverte o sprite se necessário
        if (xAxis > 0 && !isFacingRight)
        {
            //Inverte a direção do sprite
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        else if (xAxis < 0 && isFacingRight) 
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    private void Jump()
    {
        EstaNoChao = Physics2D.OverlapCircle(VerificarChao.position, 0.1f, chao);

        if(Input.GetButtonDown("Jump") && EstaNoChao == true)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = false;
            escalando = false; // Parando de escalar
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void FixedUpdate()
    {
        if(escalando)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidade);
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }
}
