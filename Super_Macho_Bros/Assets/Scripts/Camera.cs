using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float minX, maxX;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        // Mover a câmera para seguir o jogador
        if (Player.position.x >= transform.position.x)
        {
            transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        }

        // Restringir o movimento da câmera dentro dos limites especificados
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignorar colisão com objetos na camada "enemys"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemys"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}

