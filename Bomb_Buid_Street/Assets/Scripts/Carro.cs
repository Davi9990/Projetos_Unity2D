using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour
{
   [SerializeField] Transform startPoint; // Ponto inicial
    [SerializeField] Transform endPoint; // Ponto final
    [SerializeField] float speed = 10f;
    [SerializeField] float tempoParadoThreshold = 10f; // Tempo necessário para spawnar o carro
    public SpriteRenderer sprite;

    public Rigidbody2D playerRb; // Referência ao Player Normal
    public Rigidbody2D playerRbGrande; // Referência ao Player Grande

    private bool carroSpawnado = false;

    public float tempoParadoPlayer = 0f; // Tempo parado do Player Normal
    public float tempoParadoPlayerGrande = 0f; // Tempo parado do Player Grande

    void Start()
    {
        // Encontre os jogadores na cena
        if (playerRb == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                playerRb = playerObject.GetComponent<Rigidbody2D>();
        }

        if (playerRbGrande == null)
        {
            GameObject playerObject2 = GameObject.FindGameObjectWithTag("Player_Grande");
            if (playerObject2 != null)
                playerRbGrande = playerObject2.GetComponent<Rigidbody2D>();
        }

        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();

        sprite.enabled = false; // Inicia o sprite como invisível
    }

    void Update()
    {
        Spawnando();
        MoveObjects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("EndPoint2") ||
            collision.gameObject.CompareTag("Bola") ||
            collision.gameObject.CompareTag("Player_Grande"))
        {
            transform.position = startPoint.position;
            carroSpawnado = false;
            sprite.enabled = false;
            tempoParadoPlayer = 0f;
            tempoParadoPlayerGrande = 0f;
        }
    }

    public void Spawnando()
    {
        bool playerParado = false;

        // Verifica o estado do Player Normal apenas se ele estiver ativo
        if (playerRb != null && playerRb.gameObject.activeInHierarchy)
        {
            float playerSpeed = playerRb.velocity.magnitude;

            if (playerSpeed == 0)
                tempoParadoPlayer += Time.deltaTime;
            else
                tempoParadoPlayer = 0f;

            playerParado = tempoParadoPlayer >= tempoParadoThreshold;
        }
        else
        {
            tempoParadoPlayer = 0f; // Reseta o tempo parado quando o jogador está desativado
        }

        // Verifica o estado do Player Grande apenas se ele estiver ativo
        if (playerRbGrande != null && playerRbGrande.gameObject.activeInHierarchy)
        {
            float playerSpeed2 = playerRbGrande.velocity.magnitude;

            if (playerSpeed2 == 0)
                tempoParadoPlayerGrande += Time.deltaTime;
            else
                tempoParadoPlayerGrande = 0f;

            playerParado = tempoParadoPlayerGrande >= tempoParadoThreshold;
        }
        else
        {
            tempoParadoPlayerGrande = 0f; // Reseta o tempo parado quando o jogador está desativado
        }

        // Spawn do carro
        if (!carroSpawnado && playerParado)
        {
            sprite.enabled = true;
            carroSpawnado = true;
        }
    }

    void MoveObjects()
    {
        if (carroSpawnado)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
