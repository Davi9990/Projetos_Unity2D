using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma2 : MonoBehaviour
{
    public enum Direction { Vertical, Horizontal }  // Enumeração para definir a direção de movimento da plataforma
    public Direction moveDirection = Direction.Vertical;  // Variável para escolher a direção de movimento (Vertical ou Horizontal)

    public float speed = 2f;  // Velocidade de movimento da plataforma
    public float distance = 3f;  // Distância que a plataforma se moverá antes de mudar de direção

    private Vector3 startPos;  // Posição inicial da plataforma
    private bool movingForward = true;  // Controle da direção do movimento (frente ou trás)

    void Start()
    {
        startPos = transform.position;  // Armazena a posição inicial da plataforma
    }

    void Update()
    {
        MovePlatform();  // Chama o método de movimentação da plataforma a cada frame
    }

    void MovePlatform()
    {
        Vector3 targetPos;

        // Define a posição alvo com base na direção de movimento e se a plataforma está se movendo para frente ou para trás
        if (moveDirection == Direction.Vertical)
        {
            targetPos = movingForward ? startPos + Vector3.up * distance : startPos;
        }
        else // Horizontal
        {
            targetPos = movingForward ? startPos + Vector3.right * distance : startPos;
        }

        // Move a plataforma em direção à posição alvo com a velocidade definida
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Quando a plataforma atingir a posição alvo, ela inverte a direção do movimento
        if (transform.position == targetPos)
        {
            movingForward = !movingForward;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se o jogador colidir com a plataforma, ele se torna filho da plataforma
        // Isso faz com que o jogador se mova junto com a plataforma
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Quando o jogador sair da plataforma, ele não é mais filho da plataforma
        // Isso permite que o jogador se mova independentemente da plataforma
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
