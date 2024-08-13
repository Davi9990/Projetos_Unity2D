using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoKoopa : MonoBehaviour
{
    public Transform pontoA;       // Ponto A que define um dos limites do movimento do inimigo
    public Transform pontoB;       // Ponto B que define o outro limite do movimento do inimigo
    public float velocidade = 2f;  // Velocidade de movimento do inimigo

    private Vector3 pontoAtual;    // Ponto atual para o qual o inimigo está se movendo
    private bool viradoDireita = true; // Indica se o inimigo está virado para a direita
    private Rigidbody2D rb;        // Referência ao Rigidbody2D do inimigo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicializa a referência ao Rigidbody2D do inimigo
        pontoAtual = pontoB.position;     // Define o ponto inicial para o movimento como sendo o ponto B
    }

    void FixedUpdate()
    {
        // Mover o inimigo em direção ao ponto atual
        Vector2 novaPosicao = Vector2.MoveTowards(rb.position, pontoAtual, velocidade * Time.fixedDeltaTime);
        rb.MovePosition(novaPosicao);

        // Verificar se o inimigo chegou ao ponto atual
        if (Vector2.Distance(rb.position, pontoAtual) < 0.1f)
        {
            // Trocar o ponto atual para o próximo ponto
            if (pontoAtual == pontoA.position)
            {
                pontoAtual = pontoB.position; // Se o inimigo está no ponto A, define o ponto B como o próximo destino
            }
            else
            {
                pontoAtual = pontoA.position; // Se o inimigo está no ponto B, define o ponto A como o próximo destino
            }

            // Virar o inimigo na direção oposta
            Virar();
        }
    }

    void Virar()
    {
        viradoDireita = !viradoDireita;  // Alterna a direção em que o inimigo está virado
        Vector3 escala = transform.localScale;
        escala.x *= -1;                  // Inverte a escala no eixo X para virar o inimigo
        transform.localScale = escala;   // Aplica a nova escala ao inimigo
    }
}
