using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFogo : MonoBehaviour
{
    public float speed = 10f; // Velocidade da bola de fogo

    private void Start()
    {
        // Move a bola de fogo para a esquerda ao sair da boca do boss
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
}
