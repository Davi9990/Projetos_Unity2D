using UnityEngine;
using UnityEngine.SceneManagement; // Necess�rio para carregar cenas

public class PortaFinal : MonoBehaviour
{
    public string nomeDaCena; // Nome da cena a ser carregada

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            JogadorInventario inventario = collision.gameObject.GetComponent<JogadorInventario>();
            if (inventario != null && inventario.TemChave())
            {
                inventario.UsarChave(); // Remove uma chave do invent�rio do jogador
                Destroy(gameObject); // Destroi a porta
                CarregarCena(); // Carrega a cena especificada
            }
        }
    }

    private void CarregarCena()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
