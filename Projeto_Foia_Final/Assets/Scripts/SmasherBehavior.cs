using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherBehavior : MonoBehaviour
{
    public enum Estado { Patrulha, Smash }
    public Estado estadoAtual = Estado.Patrulha;

    public float velocidadeHorizontal = 2.0f;
    public float alturaDoPulo = 5.0f;
    public float forcaSmash = 8.0f; // Força do Smash em direção ao jogador
    public float raioDeteccaoSmash = 5.0f; // Raio de detecção do jogador
    public LayerMask layerJogador; // Layer para o jogador
    public LayerMask layerChao; // Layer para o chão

    public Transform groundCheck; // Objeto vazio para verificar o chão
    public float raioDeteccaoChao = 0.2f; // Raio da detecção do chão
    public float distanciaDePatrulha = 3.0f;

    private Rigidbody2D rb;
    private bool estaNoChao = false;
    private bool movendoParaDireita = true;
    private Vector2 posicaoInicial;
    private Transform jogador;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position; // Define o rastro de patrulha inicialmente
    }

    void Update()
    {
        VerificarChao();
        DetectarJogador();

        switch (estadoAtual)
        {
            case Estado.Patrulha:
                if (estaNoChao)
                {
                    Patrulhar();
                }
                break;

            case Estado.Smash:
                if (estaNoChao)
                {
                    Smash();
                }
                break;
        }
    }

    void Patrulhar()
    {
        // Determina a direção do pulo
        float direcaoHorizontal = movendoParaDireita ? velocidadeHorizontal : -velocidadeHorizontal;

        // Aplicar o pulo em uma trajetória parabólica
        rb.velocity = new Vector2(direcaoHorizontal, alturaDoPulo);

        // Verifica se o inimigo atingiu os limites do rastro de patrulha e inverte a direção
        if (movendoParaDireita && transform.position.x >= posicaoInicial.x + distanciaDePatrulha)
        {
            movendoParaDireita = false;
        }
        else if (!movendoParaDireita && transform.position.x <= posicaoInicial.x - distanciaDePatrulha)
        {
            movendoParaDireita = true;
        }
    }

    void DetectarJogador()
    {
        // Verifica se há um jogador no raio de detecção
        Collider2D jogadorDetectado = Physics2D.OverlapCircle(transform.position, raioDeteccaoSmash, layerJogador);

        if (jogadorDetectado != null)
        {
            jogador = jogadorDetectado.transform; // Guarda a referência do jogador
            estadoAtual = Estado.Smash; // Muda o estado para Smash
            Debug.Log("Jogador detectado! Iniciando Smash!");
        }
    }

    void Smash()
    {
        // Calcula a direção para o jogador
        Vector2 direcaoParaJogador = (jogador.position - transform.position).normalized;

        // Aplica o pulo com força extra em direção ao jogador
        rb.velocity = new Vector2(direcaoParaJogador.x * forcaSmash, alturaDoPulo);

        // Se o jogador sair do raio de detecção, volta à patrulha
        if (Vector2.Distance(transform.position, jogador.position) > raioDeteccaoSmash)
        {
            estadoAtual = Estado.Patrulha;
            jogador = null; // Limpa a referência do jogador
            ReposicionarRastroDePatrulha(); // Reposiciona o rastro de patrulha
            Debug.Log("Jogador fora do alcance, voltando para Patrulha.");
        }
    }

    void ReposicionarRastroDePatrulha()
    {
        // Reposiciona o rastro de patrulha para a posição atual do inimigo
        posicaoInicial = transform.position;
        movendoParaDireita = true; // Começa a patrulhar para a direita
    }

    void VerificarChao()
    {
        // Verifica se há chão usando um OverlapCircle no ponto de groundCheck
        estaNoChao = Physics2D.OverlapCircle(groundCheck.position, raioDeteccaoChao, layerChao);
    }

    // Desenha o Gizmo na Scene View para visualizar o OverlapCircle e a área de patrulha
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, raioDeteccaoChao); // Visualiza o raio do OverlapCircle
        }

        // Desenha o raio de detecção para o Smash
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raioDeteccaoSmash); // Visualiza o raio de detecção do Smash

        // Desenha a área de patrulha (rastro de patrulha)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(posicaoInicial.x - distanciaDePatrulha, transform.position.y), new Vector2(posicaoInicial.x + distanciaDePatrulha, transform.position.y));
    }
}
