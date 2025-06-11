using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;  // Referência ao jogador
    public float smoothSpeed = 5f; // Velocidade de suavização
    public Vector3 offset;    // Distância entre a câmera e o jogador

    void LateUpdate()
    {
        if (player == null) return; // Evita erros caso o jogador não esteja atribuído

        // Calcula a posição desejada
        Vector3 targetPosition = player.position + offset;

        // Suaviza a movimentação da câmera
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
