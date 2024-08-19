using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador
    public float smoothness = 0.125f; // Suavidade da câmera
    public Vector3 offset; // Deslocamento para ajustar a posição da câmera em relação ao jogador

    private void FixedUpdate()
    {
        if (player != null)
        {
            // Calcula a posição desejada da câmera
            Vector3 targetPosition = player.position + offset;

            // Aplica suavemente o movimento da câmera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
        }
    }
}
