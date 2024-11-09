using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour
{
    [SerializeField] Transform startPoint; // Ponto inicial
    [SerializeField] Transform endPoint; // Ponto final
    [SerializeField] float speed = 10f;
    [SerializeField] float tempoParado; // Agora � SerializeField para ser visualizado no Inspector
    public GameObject objeto; // Refer�ncia ao objeto do inimigo
    public SpriteRenderer sprite;

    private Rigidbody2D playerRb; // Refer�ncia ao Rigidbody2D do jogador
    private bool carroSpawnado = false; // Nova vari�vel para verificar se o carro foi spawnado

    void Start()
    {
        // Encontre o Rigidbody2D do jogador na cena
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerRb = playerObject.GetComponent<Rigidbody2D>();
        }

        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false; // Inicia o sprite como invis�vel
    }

    void Update()
    {
        Spawnando();
        MoveObjects(); // Move o carro independentemente do estado do jogador
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o carro colidir com um dos objetos especificados
        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("EndPoint2") ||
            collision.gameObject.CompareTag("Bola"))
        {
            // Retorna o carro ao ponto inicial
            transform.position = startPoint.position; // Redefine a posi��o para o ponto inicial
            carroSpawnado = false; // Reinicia o estado de spawn para permitir que ele seja spawnado novamente
            sprite.enabled = false; // Torna o sprite invis�vel novamente
            tempoParado = 0; // Reinicia o tempo parado
        }
    }

    public void Spawnando()
    {
        // Verifica se o Rigidbody2D do jogador foi encontrado
        if (playerRb != null)
        {
            // Obt�m a velocidade do jogador
            float playerSpeed = playerRb.velocity.magnitude; // Usamos magnitude para obter a velocidade total

            //Debug.Log("Velocidade do jogador: " + playerSpeed); // Debug para verificar a velocidade do jogador

            if (playerSpeed == 0)
            {
                tempoParado += Time.deltaTime; // Adiciona tempo em segundos
                //Debug.Log("Tempo parado: " + tempoParado); // Log para verificar tempo parado

                if (!carroSpawnado && tempoParado >= 10f) // Verifica se passou 10 segundos e o carro ainda n�o foi spawnado
                {
                    sprite.enabled = true; // Ativa o sprite do inimigo
                    carroSpawnado = true; // Marca que o carro foi spawnado
                }
            }
            else
            {
                //Debug.Log("Jogador se movendo. Reiniciando tempo parado.");
                tempoParado = 0; // Reinicia o tempo quando o jogador se move
            }
        }
        else
        {
            //Debug.LogWarning("Rigidbody2D do jogador n�o encontrado!");
        }
    }

    void MoveObjects()
    {
        // Move o carro se ele j� tiver sido spawnado
        if (carroSpawnado)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, endPoint.position) < 0.1f) // Para de se mover ao chegar ao ponto final
            {
                // Aqui voc� pode adicionar qualquer a��o adicional que queira fazer quando o carro chegar ao final
                Destroy(gameObject); // Exemplo: destr�i o carro
            }
        }
    }
}
