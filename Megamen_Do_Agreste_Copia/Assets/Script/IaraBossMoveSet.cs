using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class IaraBossMoveSet : MonoBehaviour
{
    // Primeiro Ataque
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 15f;
    private Vector3 pontoAtual;
    private Vector2 direction;
    private bool Flip = true;
    private Rigidbody2D rb;
    public float amplitude = 2f;
    public float frequencia = 2f;
    public int Moves = 0;
    public float TempoRecargaTiro = 2f;
    public GameObject JatoPrefab;
    public Transform PontoDisparo;
    public float TempoDeVidaProjetil;
    private float tempoUltimoTiro;
    public float velocidadeProjettil = 10f;
    private bool JaContouPonto = false;
    public Transform Jogador;

    // Segundo Ataque
    public Transform Redemoinho1;
    public Transform Redemoinho2;
    public float TempoDeVidaRedemoinho;
    public GameObject agua;

    // Controle de padrões
    private bool trocandoPadrao = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Define a posição inicial e o ponto atual
        transform.position = pontoB.position;
        pontoAtual = pontoB.position;
        direction = (pontoA.position - transform.position).normalized;
    }

    void Update()
    {
        TrocaDePadroes();
    }

    public void TrocaDePadroes()
    {
        if (Moves <= 4) // Primeiro padrão: Nadando e Atirando
        {
            NadandoeAtirando();
            AtirandoEmCorno();
        }
        else if (Moves <= 5) // Segundo padrão: RedemoinhoBrabo + Movimentação
        {
            if (!trocandoPadrao)
            {
                ReiniciarEstado(); // Reseta variáveis essenciais para o novo padrão
            }
            NadandoeAtirando();
            RedemoinhoBrabo();
        }
    }

    public void NadandoeAtirando()
    {
        MoveObjeto();

        // Verifica se chegou próximo o suficiente ao ponto atual
        if (Mathf.Abs(transform.position.x - pontoAtual.x) < 0.5f) // Verificação no eixo X
        {
            // Troca o ponto de destino
            if (pontoAtual == pontoA.position)
            {
                pontoAtual = pontoB.position;
            }
            else
            {
                pontoAtual = pontoA.position;
            }

            // Atualiza a direção
            direction = (pontoAtual - transform.position).normalized;

            // Realiza o Flip
            Virar();

            // Libera contagem de ponto
            JaContouPonto = false;
        }
    }

    public void AtirandoEmCorno()
    {
        if (Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            GameObject projetil = Instantiate(JatoPrefab, PontoDisparo.position, Quaternion.identity);

            Vector2 direction = Flip ? Vector2.left : Vector2.right;

            Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0;
                rb.velocity = direction * velocidadeProjettil;
            }

            Destroy(projetil, TempoDeVidaProjetil);
            tempoUltimoTiro = Time.time;
        }
    }

    public void RedemoinhoBrabo()
    {
        if (Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            GameObject newCirculo = Instantiate(agua, Redemoinho1.position, Quaternion.identity);
            GameObject newCirculo2 = Instantiate(agua, Redemoinho2.position, Quaternion.identity);

            Vector2 direction1 = (Jogador.position - Redemoinho1.position).normalized;
            Vector2 direction2 = (Jogador.position - Redemoinho2.position).normalized;

            Rigidbody2D AguaRb = newCirculo.GetComponent<Rigidbody2D>();
            Rigidbody2D AguaRb2 = newCirculo2.GetComponent<Rigidbody2D>();

            if (AguaRb != null && AguaRb2 != null)
            {
                AguaRb.gravityScale = 10;
                AguaRb2.gravityScale = 10;
                AguaRb.velocity = direction1 * velocidadeProjettil;
                AguaRb2.velocity = direction2 * velocidadeProjettil;
            }

            tempoUltimoTiro = Time.time;
            Destroy(newCirculo, TempoDeVidaRedemoinho);
            Destroy(newCirculo2, TempoDeVidaRedemoinho);
        }
    }

    void MoveObjeto()
    {
        // Calcula a velocidade horizontal
        float horizontalVelocity = direction.x * velocidade;

        // Adiciona o movimento sinusoidal no eixo Y
        float verticalOffset = Mathf.Sin(Time.time * frequencia) * amplitude;

        // Define a velocidade do Rigidbody2D
        rb.velocity = new Vector2(horizontalVelocity, verticalOffset);
    }

    void ReiniciarEstado()
    {
        // Reseta a direção e o ponto atual
        pontoAtual = transform.position.x > (pontoA.position.x + pontoB.position.x) / 2 ? pontoA.position : pontoB.position;
        direction = (pontoAtual - transform.position).normalized;

        // Garante que o Rigidbody2D não esteja travado
        rb.velocity = Vector2.zero;

        // Reseta o Flip para garantir que o boss se oriente corretamente
        if ((pontoAtual.x < transform.position.x && Flip) || (pontoAtual.x > transform.position.x && !Flip))
        {
            Virar();
        }

        trocandoPadrao = true; // Marca o estado como em transição
    }

    void Virar()
    {
        // Verifica a direção e realiza o Flip
        if ((pontoAtual.x < transform.position.x && !Flip) || (pontoAtual.x > transform.position.x && Flip))
        {
            Flip = !Flip; // Inverte o estado de Flip
            Vector3 escala = transform.localScale;
            escala.x *= -1; // Inverte a escala no eixo X
            transform.localScale = escala;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PontoA") || collision.gameObject.CompareTag("PontoB"))
        {
            if (!JaContouPonto)
            {
                Moves += 1;
                JaContouPonto = true;
            }
        }
    }
}
