using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float verticalOffset = 2f;

    void Update()
    {
        if (player != null)
        {
            // Acompanha apenas na altura (eixo Y), mantém o X atual
            transform.position = new Vector3(transform.position.x, player.position.y + verticalOffset, transform.position.z);
        }
    }
}
