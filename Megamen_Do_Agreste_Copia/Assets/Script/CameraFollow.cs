using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float suavidade = 0.125f; //Suavidade da camera
    public Vector3 offset; //Deslocamento para ajustar a posição da camera em relação ao jogador


    private void FixedUpdate()
    {
        if(player != null)
        {
            //Calcula a posição desejada da camera
            Vector3 targetPosition = player.position + offset;

            //Aplica suavemente o movimento da camera
            transform.position = Vector3.Lerp(transform.position, targetPosition, suavidade);
        }
    }
}
