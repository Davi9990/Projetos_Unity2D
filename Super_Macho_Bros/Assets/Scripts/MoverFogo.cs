using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFogo : MonoBehaviour
{
    public float speed = 2.5f; // Velocidade de movimento do disparo
    public float lifeTime = 5f; // Tempo de vida da prefab em segundos

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // Destr�i o game object ap�s o tempo de vida definido
    }

    void Update()
    {
        MoveFireball();
    }

    private void MoveFireball()
    {
        // Movimenta o disparo para a direita
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Causar dano ao jogador (adicione o c�digo de dano aqui)
            Destroy(gameObject); // Destroi o disparo ap�s acertar o jogador
        }
        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroi o disparo ao acertar o ch�o ou uma parede
        }
    }
}
