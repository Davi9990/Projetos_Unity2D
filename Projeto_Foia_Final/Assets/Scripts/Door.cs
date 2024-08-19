using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            JogadorInventario inventario = collision.gameObject.GetComponent<JogadorInventario>();
            if (inventario != null && inventario.TemChave())
            {
                inventario.UsarChave(); // Remove uma chave do inventário do jogador
                Destroy(gameObject); // Destroi a porta
            }
        }
    }
}
