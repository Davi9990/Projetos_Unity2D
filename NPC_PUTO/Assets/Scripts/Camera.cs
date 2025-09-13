using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float suavidade = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, suavidade);
        }
    }
}
