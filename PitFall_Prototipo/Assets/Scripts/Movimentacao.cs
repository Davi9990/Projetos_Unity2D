using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    //Movimenta��o
    public float velocidade;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    //Pulos
    public int Pulos;
    public Transform VerificarChao;
    public bool EstaNoChao;
    public LayerMask chao;
    public float JumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        //Captura o eixo horizontal
        float xAxis = Input.GetAxisRaw("Horizontal");

        //Atualiza a velocidade no eixo X, mantendo a velocidade Y existente
        rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);

        //Atualiza a dire��o do movimento e inverte o sprite se necess�rio
        if (xAxis > 0 && !isFacingRight)
        {
            //Inverte a dire��o do sprite
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
}