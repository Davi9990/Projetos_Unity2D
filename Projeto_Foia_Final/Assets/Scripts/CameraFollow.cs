using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Refer�ncia ao Transform do jogador
    public float smoothness = 0.125f; // Suavidade da c�mera
    public Vector3 offset; // Deslocamento para ajustar a posi��o da c�mera em rela��o ao jogador

    private void FixedUpdate()
    {
        if (player != null)
        {
            // Calcula a posi��o desejada da c�mera
            Vector3 targetPosition = player.position + offset;

            // Aplica suavemente o movimento da c�mera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
        }
    }
}
