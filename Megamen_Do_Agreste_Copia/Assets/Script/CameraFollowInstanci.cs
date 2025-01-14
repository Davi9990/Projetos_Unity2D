using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowInstanci : MonoBehaviour
{
   public Transform player; // Refer�ncia ao Transform do jogador
    public float suavidade = 0.125f; // Suavidade da c�mera
    public Vector3 offset; // Deslocamento para ajustar a posi��o da c�mera em rela��o ao jogador

    private void Start()
    {
        // Tenta encontrar o jogador pelo tag na inicializa��o
        FindPlayer();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            // Se o jogador ainda n�o foi encontrado, tenta localiz�-lo
            FindPlayer();
        }
        else
        {
            // Calcula a posi��o desejada da c�mera
            Vector3 targetPosition = player.position + offset;

            // Aplica suavemente o movimento da c�mera
            transform.position = Vector3.Lerp(transform.position, targetPosition, suavidade);
        }
    }

    private void FindPlayer()
    {
        // Procura o jogador na cena baseado em sua tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Atualiza a refer�ncia do jogador
        }
    }
}
