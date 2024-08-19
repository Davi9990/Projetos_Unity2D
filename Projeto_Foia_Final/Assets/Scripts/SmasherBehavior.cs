using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherBehavior : MonoBehaviour
{
    public enum Estado { Patrulha, Smash }
    public Estado estadoAtual = Estado.Patrulha;

    public float velocidadeHorizontal = 2.0f;
    public float alturaDoPulo = 5.0f;
    public float forcaSmash = 8.0f; // For�a do Smash em dire��o ao jogador
    public float raioDeteccaoSmash = 5.0f; // Raio de detec��o do jogador
    public LayerMask layerJogador; // Layer para o jogador
    public LayerMask layerChao; // Layer para o ch�o

    public Transform groundCheck; // Objeto vazio para verificar o ch�o
    public float raioDeteccaoChao = 0.2f; // Raio da detec��o do ch�o
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
        // Determina a dire��o do pulo
        float direcaoHorizontal = movendoParaDireita ? velocidadeHorizontal : -velocidadeHorizontal;

        // Aplicar o pulo em uma trajet�ria parab�lica
        rb.velocity = new Vector2(direcaoHorizontal, alturaDoPulo);

        // Verifica se o inimigo atingiu os limites do rastro de patrulha e inverte a dire��o
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
        // Verifica se h� um jogador no raio de detec��o
        Collider2D jogadorDetectado = Physics2D.OverlapCircle(transform.position, raioDeteccaoSmash, layerJogador);

        if (jogadorDetectado != null)
        {
            jogador = jogadorDetectado.transform; // Guarda a refer�ncia do jogador
            estadoAtual = Estado.Smash; // Muda o estado para Smash
            Debug.Log("Jogador detectado! Iniciando Smash!");
        }
    }

    void Smash()
    {
        // Calcula a dire��o para o jogador
        Vector2 direcaoParaJogador = (jogador.position - transform.position).normalized;

        // Aplica o pulo com for�a extra em dire��o ao jogador
        rb.velocity = new Vector2(direcaoParaJogador.x * forcaSmash, alturaDoPulo);

        // Se o jogador sair do raio de detec��o, volta � patrulha
        if (Vector2.Distance(transform.position, jogador.position) > raioDeteccaoSmash)
        {
            estadoAtual = Estado.Patrulha;
            jogador = null; // Limpa a refer�ncia do jogador
            ReposicionarRastroDePatrulha(); // Reposiciona o rastro de patrulha
            Debug.Log("Jogador fora do alcance, voltando para Patrulha.");
        }
    }

    void ReposicionarRastroDePatrulha()
    {
        // Reposiciona o rastro de patrulha para a posi��o atual do inimigo
        posicaoInicial = transform.position;
        movendoParaDireita = true; // Come�a a patrulhar para a direita
    }

    void VerificarChao()
    {
        // Verifica se h� ch�o usando um OverlapCircle no ponto de groundCheck
        estaNoChao = Physics2D.OverlapCircle(groundCheck.position, raioDeteccaoChao, layerChao);
    }

    // Desenha o Gizmo na Scene View para visualizar o OverlapCircle e a �rea de patrulha
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, raioDeteccaoChao); // Visualiza o raio do OverlapCircle
        }

        // Desenha o raio de detec��o para o Smash
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raioDeteccaoSmash); // Visualiza o raio de detec��o do Smash

        // Desenha a �rea de patrulha (rastro de patrulha)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(posicaoInicial.x - distanciaDePatrulha, transform.position.y), new Vector2(posicaoInicial.x + distanciaDePatrulha, transform.position.y));
    }
}
