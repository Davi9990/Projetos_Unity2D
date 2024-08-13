using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma2 : MonoBehaviour
{
    public enum Direction { Vertical, Horizontal }  // Enumera��o para definir a dire��o de movimento da plataforma
    public Direction moveDirection = Direction.Vertical;  // Vari�vel para escolher a dire��o de movimento (Vertical ou Horizontal)

    public float speed = 2f;  // Velocidade de movimento da plataforma
    public float distance = 3f;  // Dist�ncia que a plataforma se mover� antes de mudar de dire��o

    private Vector3 startPos;  // Posi��o inicial da plataforma
    private bool movingForward = true;  // Controle da dire��o do movimento (frente ou tr�s)

    void Start()
    {
        startPos = transform.position;  // Armazena a posi��o inicial da plataforma
    }

    void Update()
    {
        MovePlatform();  // Chama o m�todo de movimenta��o da plataforma a cada frame
    }

    void MovePlatform()
    {
        Vector3 targetPos;

        // Define a posi��o alvo com base na dire��o de movimento e se a plataforma est� se movendo para frente ou para tr�s
        if (moveDirection == Direction.Vertical)
        {
            targetPos = movingForward ? startPos + Vector3.up * distance : startPos;
        }
        else // Horizontal
        {
            targetPos = movingForward ? startPos + Vector3.right * distance : startPos;
        }

        // Move a plataforma em dire��o � posi��o alvo com a velocidade definida
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Quando a plataforma atingir a posi��o alvo, ela inverte a dire��o do movimento
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
        // Quando o jogador sair da plataforma, ele n�o � mais filho da plataforma
        // Isso permite que o jogador se mova independentemente da plataforma
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
