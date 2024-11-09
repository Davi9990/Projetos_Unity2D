using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoRato : MonoBehaviour
{
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 2f;

    private Vector3 pontoAtual;
    private bool viradoDireita = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pontoAtual = pontoB.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Mover o inimigo em direção ao ponto atual
        Vector2 novaPosicao = Vector2.MoveTowards(rb.position,pontoAtual, velocidade * Time.fixedDeltaTime);
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

            Virar();
        }
    }

    void Virar()
    {
        viradoDireita = !viradoDireita;
        Vector2 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}
