using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class IaraBossMoveSet : MonoBehaviour
{
    public Transform pontoA, pontoB; // Pontos de movimentação
    public float velocidade = 15f, amplitude = 2f, frequencia = 2f;
    public int Moves = 0;

    // Objetos de Ataque
    public GameObject JatoPrefab, agua, Bolhas;
    public Transform PontoDisparo, Jogador, Redemoinho1, Redemoinho2, PontoDeBolha, PontoDeBolha2;
    public float TempoRecargaTiro = 2f, TempoDeVidaProjetil = 3f, TempoDeVidaRedemoinho = 3f, velocidadeProjettil = 10f;
    public float forcaBolha = 3f, velBolha = 2.5f, lifeTimeProjetil = 5;

    private Rigidbody2D rb;
    private Vector3 pontoAtual;
    private Vector2 direction;
    private bool Flip = true, JaContouPonto = false;
    private float tempoUltimoTiro;

    private enum EstadoBoss { NadandoAtirando, RedemoinhoBrabo, MovimentoSimples }
    private EstadoBoss estadoAtual = EstadoBoss.NadandoAtirando;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = pontoB.position;
        pontoAtual = pontoB.position;
        direction = (pontoA.position - transform.position).normalized;
    }

    void Update()
    {
        ControlarPadrao();
    }

    // Alternar entre padrões
    void ControlarPadrao()
    {
        switch (estadoAtual)
        {
            case EstadoBoss.NadandoAtirando:
                NadandoeAtirando();
                AtirarProjetil();
                if (Moves > 4) ReiniciarEstado(EstadoBoss.RedemoinhoBrabo);
                break;

            case EstadoBoss.RedemoinhoBrabo:
                NadandoeAtirando();
                RedemoinhoBrabo();
                if (Moves > 14) ReiniciarEstado(EstadoBoss.MovimentoSimples);
                break;

            case EstadoBoss.MovimentoSimples:
                NadandoeAtirando();
                AtirarBolhas();
                if (Moves > 24) ReiniciarEstado(EstadoBoss.NadandoAtirando);
                break;
        }
    }

    // Movimentação de ida e volta
    void NadandoeAtirando()
    {
        MoveObjeto();
        if (Mathf.Abs(transform.position.x - pontoAtual.x) < 0.5f)
        {
            TrocarPonto();
        }
    }

    void MoveObjeto()
    {
        // Continuar o movimento horizontal e com a variação vertical
        float horizontalVelocity = direction.x * velocidade;
        float verticalOffset = Mathf.Sin(Time.time * frequencia) * amplitude;

        rb.velocity = new Vector2(horizontalVelocity, verticalOffset);
    }

    void TrocarPonto()
    {
        pontoAtual = pontoAtual == pontoA.position ? pontoB.position : pontoA.position;
        direction = (pontoAtual - transform.position).normalized;
        Virar();
        JaContouPonto = false;
        Moves++;
    }

    // Tiro simples
    void AtirarProjetil()
    {
        if (Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            GameObject projetil = Instantiate(JatoPrefab, PontoDisparo.position, Quaternion.identity);
            Vector2 direcaoTiro = Flip ? Vector2.left : Vector2.right;

            Rigidbody2D rbProj = projetil.GetComponent<Rigidbody2D>();
            if (rbProj != null)
            {
                rbProj.gravityScale = 0;
                rbProj.velocity = direcaoTiro * velocidadeProjettil;
            }

            Destroy(projetil, TempoDeVidaProjetil);
            tempoUltimoTiro = Time.time;
        }
    }

    // Ataque de redemoinho
    void RedemoinhoBrabo()
    {
        // Removido rb.velocity = Vector2.zero para continuar movimento
        if (Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            CriarProjetil(Redemoinho1);
            CriarProjetil(Redemoinho2);
            tempoUltimoTiro = Time.time;
            Moves++; // Incrementa após a criação de projetis
        }
    }

    void CriarProjetil(Transform spawnPoint)
    {
        GameObject projetil = Instantiate(agua, spawnPoint.position, Quaternion.identity);
        Vector2 direcaoTiro = (Jogador.position - spawnPoint.position).normalized;

        Rigidbody2D rbProj = projetil.GetComponent<Rigidbody2D>();
        if (rbProj != null)
        {
            rbProj.gravityScale = 0;
            rbProj.velocity = direcaoTiro * velocidadeProjettil;
        }

        Destroy(projetil, TempoDeVidaRedemoinho);
    }

    // Tiro de bolhas com movimento saltitante
    void AtirarBolhas()
    {
        if (Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            CriarBolha(PontoDeBolha);
            CriarBolha(PontoDeBolha2);
            tempoUltimoTiro = Time.time;
            Moves++; // Incrementa após a criação de bolhas
        }
    }

    void CriarBolha(Transform spawnPoint)
    {
        GameObject bolha = Instantiate(Bolhas, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rbBolha = bolha.GetComponent<Rigidbody2D>();
        rbBolha.velocity = Vector2.up * forcaBolha;

        // Adicionando um movimento saltitante
        rbBolha.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);

        Destroy(bolha, lifeTimeProjetil);
    }

    void ReiniciarEstado(EstadoBoss novoEstado)
    {
        Moves = 0;
        estadoAtual = novoEstado;

        // Redefine o ponto atual sem parar o movimento
        if (transform.position.x > (pontoA.position.x + pontoB.position.x) / 2)
        {
            pontoAtual = pontoA.position;
        }
        else
        {
            pontoAtual = pontoB.position;
        }

        // Recalcula a direção imediatamente
        direction = (pontoAtual - transform.position).normalized;

        // Mantém o Rigidbody2D em movimento direto
        rb.velocity = new Vector2(direction.x * velocidade, Mathf.Sin(Time.time * frequencia) * amplitude);
    }

    void Virar()
    {
        if ((pontoAtual.x < transform.position.x && !Flip) || (pontoAtual.x > transform.position.x && Flip))
        {
            Flip = !Flip;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}
