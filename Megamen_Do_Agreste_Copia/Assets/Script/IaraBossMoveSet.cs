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
    public Transform PontoDisparo, Redemoinho1, Redemoinho2, PontoDeBolha, PontoDeBolha2;
    public float TempoRecargaTiro = 2f, TempoDeVidaProjetil = 3f, TempoDeVidaRedemoinho = 3f, velocidadeProjettil = 10f;
    public float forcaBolha = 3f, velBolha = 2.5f, lifeTimeProjetil = 5;

    private Rigidbody2D rb;
    private Vector3 pontoAtual;
    private Vector2 direction;
    private bool Flip = true, JaContouPonto = false;
    private float tempoUltimoTiro;

    private Transform jogador; // Referência ao Transform do jogador
    public string playerTag = "Player"; //Tag do jogador

    // Novo parâmetro para distância de detecção
    public float distanciaDeDeteccao = 10f;  // Distância mínima para o boss começar a atacar
    private enum EstadoBoss { NadandoAtirando, RedemoinhoBrabo, MovimentoSimples }
    private EstadoBoss estadoAtual = EstadoBoss.NadandoAtirando;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.position = pontoB.position;
        pontoAtual = pontoB.position;
        direction = (pontoA.position - transform.position).normalized;

        AtualizarJogador();
    }

    void Update()
    {
        if (jogador == null || !jogador.gameObject.activeInHierarchy)
        {
            AtualizarJogador(); // Atualiza a referência ao jogador, caso ele não seja mais válido
        }

        if (jogador == null) return; // Se ainda não encontrou o jogador, aguarde

        // Verifica a distância para decidir quando o boss vai atacar
        if (Vector2.Distance(transform.position, jogador.position) <= distanciaDeDeteccao)
        {
            ControlarPadrao();
        }
    }

    // Atualiza a referência do jogador usando a tag
    private void AtualizarJogador()
    {
        GameObject objetoJogador = GameObject.FindGameObjectWithTag(playerTag);
        if (objetoJogador != null)
        {
            jogador = objetoJogador.transform;
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com a tag 'Player' foi encontrado!");
        }
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
        anim.SetBool("Nadando", true);
        MoveObjeto();
        if (Mathf.Abs(transform.position.x - pontoAtual.x) < 0.5f)
        {
            TrocarPonto();
        }
    }

    void MoveObjeto()
    {
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
        if (Time.time > tempoUltimoTiro + TempoRecargaTiro)
        {
            CriarProjetil(Redemoinho1);
            CriarProjetil(Redemoinho2);
            tempoUltimoTiro = Time.time;
            Moves++;
        }
    }

    void CriarProjetil(Transform spawnPoint)
    {
        GameObject projetil = Instantiate(agua, spawnPoint.position, Quaternion.identity);
        Vector2 direcaoTiro = (jogador.position - spawnPoint.position).normalized;

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
            Moves++;
        }
    }

    void CriarBolha(Transform spawnPoint)
    {
        GameObject bolha = Instantiate(Bolhas, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rbBolha = bolha.GetComponent<Rigidbody2D>();
        rbBolha.velocity = Vector2.up * forcaBolha;
        rbBolha.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        Destroy(bolha, lifeTimeProjetil);
    }

    void ReiniciarEstado(EstadoBoss novoEstado)
    {
        Moves = 0;
        estadoAtual = novoEstado;

        anim.SetBool("Nadando", false);

        if (transform.position.x > (pontoA.position.x + pontoB.position.x) / 2)
        {
            pontoAtual = pontoA.position;
        }
        else
        {
            pontoAtual = pontoB.position;
        }

        direction = (pontoAtual - transform.position).normalized;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciaDeDeteccao);
    }
}
