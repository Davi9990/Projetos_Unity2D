using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra_Pulando : MonoBehaviour
{
    private Rigidbody2D rb;
    public float JumpForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pulo"))
        {
            // Encontra o jogador pela tag "Player"
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                // Calcula a direção do pulo em relação ao jogador
                Vector2 jumpDirection = (playerObject.transform.position - transform.position).normalized;

                // Aplica a força no Rigidbody2D
                rb.velocity = new Vector2(jumpDirection.x, 1) * JumpForce;
            }
            else
            {
                Debug.LogWarning("Nenhum objeto com a tag 'Player' foi encontrado!");
            }
        }
    }
}
