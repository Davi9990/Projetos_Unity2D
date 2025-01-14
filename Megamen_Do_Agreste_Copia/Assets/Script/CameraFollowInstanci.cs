using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowInstanci : MonoBehaviour
{
   public Transform player; // Referência ao Transform do jogador
    public float suavidade = 0.125f; // Suavidade da câmera
    public Vector3 offset; // Deslocamento para ajustar a posição da câmera em relação ao jogador

    private void Start()
    {
        // Tenta encontrar o jogador pelo tag na inicialização
        FindPlayer();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            // Se o jogador ainda não foi encontrado, tenta localizá-lo
            FindPlayer();
        }
        else
        {
            // Calcula a posição desejada da câmera
            Vector3 targetPosition = player.position + offset;

            // Aplica suavemente o movimento da câmera
            transform.position = Vector3.Lerp(transform.position, targetPosition, suavidade);
        }
    }

    private void FindPlayer()
    {
        // Procura o jogador na cena baseado em sua tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Atualiza a referência do jogador
        }
    }
}
