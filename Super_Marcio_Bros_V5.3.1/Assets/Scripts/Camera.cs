using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minX, maxX;
    

    private void Start()
    {
        // Certifique-se de que o jogador � encontrado e referenciado
        player = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (player == null) return; // Se o jogador n�o estiver definido, n�o fa�a nada

        // Atualizar a posi��o da c�mera para seguir o jogador
        float newX = Mathf.Clamp(player.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void UpdatePlayer(Transform newPlayer)
    {
        player = newPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignorar colis�o com objetos na camada "Enemys"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemys"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}

