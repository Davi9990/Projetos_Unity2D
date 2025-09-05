using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom_Red : MonoBehaviour
{
    public float speed = 5f;
    public Transform Player;
    public Transform cantoSuperiorDireito; // ponto de patrulha
    public float rangePerseguicao = 20f;
    public float distanciaRaycast = 0.6f; // distância para detectar parede
    public LayerMask layerParede; // para raycast ignorar outros objetos

    private Rigidbody2D rb;
    private Vector2 direcaoAtual;

    private enum Estado { Patrulha, Perseguir }
    private Estado estadoAtual = Estado.Patrulha;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Escolhe uma direção inicial aleatória
        Vector2[] direcoesIniciais = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        direcaoAtual = direcoesIniciais[Random.Range(0, direcoesIniciais.Length)];
    }

    void Update()
    {
        AtualizarEstado();
    }

    void FixedUpdate()
    {
        // Move o inimigo
        rb.velocity = direcaoAtual * speed;
    }

    void AtualizarEstado()
    {
        float distance = Vector2.Distance(Player.position, transform.position);
        estadoAtual = (distance <= rangePerseguicao) ? Estado.Perseguir : Estado.Patrulha;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Parede"))
        {
            EscolherNovaDirecao();
        }
    }

    void EscolherNovaDirecao()
    {
        Vector2 alvo = (estadoAtual == Estado.Perseguir) ? (Vector2)Player.position : (Vector2)cantoSuperiorDireito.position;

        Vector2 melhorDirecao = direcaoAtual;
        float menorDistancia = Mathf.Infinity;

        // Inclui diagonais para movimento mais natural
        Vector2[] direcoes = {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right,
            (Vector2.up + Vector2.left).normalized,
            (Vector2.up + Vector2.right).normalized,
            (Vector2.down + Vector2.left).normalized,
            (Vector2.down + Vector2.right).normalized
        };

        foreach (Vector2 direcao in direcoes)
        {
            // Evita voltar para trás
            if (direcao == -direcaoAtual) continue;

            // Verifica se a direção está livre
            if (Physics2D.Raycast(transform.position, direcao, distanciaRaycast, layerParede))
                continue;

            // Calcula a distância ao alvo
            Vector2 posSimulada = (Vector2)transform.position + direcao;
            float dist = Vector2.Distance(posSimulada, alvo);

            if (dist < menorDistancia)
            {
                menorDistancia = dist;
                melhorDirecao = direcao;
            }
        }

        // Se nenhuma direção melhor, inverte
        if (melhorDirecao == direcaoAtual)
        {
            melhorDirecao = -direcaoAtual;
        }

        direcaoAtual = melhorDirecao;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangePerseguicao);

        // Desenha direção atual
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + direcaoAtual * distanciaRaycast);
        }
    }
}
