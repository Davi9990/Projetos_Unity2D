using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minX, maxX;
    

    private void Start()
    {
        // Certifique-se de que o jogador é encontrado e referenciado
        player = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (player == null) return; // Se o jogador não estiver definido, não faça nada

        // Atualizar a posição da câmera para seguir o jogador
        float newX = Mathf.Clamp(player.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void UpdatePlayer(Transform newPlayer)
    {
        player = newPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignorar colisão com objetos na camada "Enemys"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemys"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}

