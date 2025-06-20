using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePatrulheiro : MonoBehaviour
{

    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 2f;

    private Vector3 pontoAtual;
    private bool virandoDireita = true;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pontoAtual = pontoB.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //Mover o inimigo em direção ao ponto atual
        Vector2 novaPosicao = Vector2.MoveTowards(rb.position, pontoAtual, velocidade * Time.deltaTime);
        rb.MovePosition(novaPosicao);

        if(Vector2.Distance(rb.position, pontoAtual) < 0.1f)
        {
            //Trocar o ponto atual
            if(pontoAtual == pontoA.position)
            {
                pontoAtual = pontoB.position;
            }
            else
            {
                pontoAtual = pontoA.position;
            }

            virar();
        }
    }

    void virar()
    {
        virandoDireita = !virandoDireita;
        Vector2 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}
