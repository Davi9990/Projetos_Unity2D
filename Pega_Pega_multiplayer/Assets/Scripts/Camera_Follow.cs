using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform Player;
    public float suavidade = 0.150f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        if(Player != null)
        {
            //Calcula a posição desejada da camera
            Vector3 targerPosition = Player.position + offset;

            //Aplica suavemente o movimento da camera
            transform.position = Vector3.Lerp(transform.position, targerPosition, suavidade);   
        }
    }
}
