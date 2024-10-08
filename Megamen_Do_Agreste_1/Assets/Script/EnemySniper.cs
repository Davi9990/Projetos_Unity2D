using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniper : MonoBehaviour
{
    public float alcanceTiro = 15f;   // Dist�ncia em que o inimigo come�a a atirar no jogador
    public float alcanceFuga = 5f;     // Dist�ncia em que o inimigo come�a a fugir do jogador
    public float velocidadeMovimento = 2f; // Velocidade de movimento do inimigo
    public float tempoRecargaTiro = 1f; // Tempo de recarga entre disparos
    public GameObject prefabProjetil; // Prefab do projetil
    public Transform pontoDisparo;    // Ponto de onde o projetil � disparado
    public float velocidadeProjetil = 10f; // Velocidade do projetil
    public float tempoVidaProjetil = 5f; // Tempo de vida do projetil em segundos
    private SpriteRenderer render; // Refer�ncia do sprite para o flip


    private float tempoUltimoTiro;
    private Transform jogador;

    private Vector3 pontoDisparoOffset; // Guarda o offset original do ponto de disparo

    void Start()
    {
        // Encontra o jogador pela tag "Player"
        GameObject objetoJogador = GameObject.FindGameObjectWithTag("Player");
        if (objetoJogador != null)
        {
            jogador = objetoJogador.transform;
        }

        render = GetComponent<SpriteRenderer>();

        pontoDisparoOffset = pontoDisparo.localPosition;
    }

    void Update()
    {
        if (jogador == null)
        {
            return;
        }

        // Calcula a dist�ncia entre o inimigo e o jogador
        float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);

        Flip();

        // Se o jogador estiver dentro do raio de fuga, o inimigo foge
        if (distanciaParaJogador <= alcanceFuga)
        {
            FugirDoJogador();
            
        }
        // Se o jogador estiver dentro do raio de tiro, o inimigo atira
        else if (distanciaParaJogador <= alcanceTiro)
        {
            AtirarNoJogador();
        }
    }

    void FugirDoJogador()
    {
        // Move o inimigo na dire��o oposta ao jogador
        Vector2 direcao = (transform.position - jogador.position).normalized;
        transform.position += (Vector3)(direcao * velocidadeMovimento * Time.deltaTime);
    }

    void AtirarNoJogador()
    {
        if (Time.time > tempoUltimoTiro + tempoRecargaTiro)
        {
            // Cria o projetil na posi��o do ponto de disparo e na dire��o do jogador
            GameObject projetil = Instantiate(prefabProjetil, pontoDisparo.position, Quaternion.identity);
            Vector2 direcao = (jogador.position - pontoDisparo.position).normalized;
            Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0; // Desativa a gravidade do projetil
                rb.velocity = direcao * velocidadeProjetil;
            }
            Destroy(projetil, tempoVidaProjetil);
            tempoUltimoTiro = Time.time;
        }
    }

    public void Flip()
    {
        // Verifica a posi��o do jogador em rela��o ao inimigo
        if (jogador != null)
        {
            if (jogador.position.x < transform.position.x)
            {
                // Se o jogador estiver � esquerda, vira o inimigo para a direita
                render.flipX = true;
                pontoDisparo.localPosition = new Vector3(pontoDisparoOffset.x,pontoDisparoOffset.y,pontoDisparoOffset.z);
            }
            else
            {
                // Se o jogador estiver � direita, vira o inimigo para a esquerda
                render.flipX = false;
                pontoDisparo.localPosition = -pontoDisparoOffset;
            }
        }
    }
}