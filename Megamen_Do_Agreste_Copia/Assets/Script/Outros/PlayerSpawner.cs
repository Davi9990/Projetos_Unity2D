using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab do jogador
    public string[] fasesParaInstanciar; // Nomes das fases onde o jogador deve ser reposicionado
    private static GameObject currentPlayerInstance; // Referência única ao jogador

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Conecta o método ao evento de cena carregada

        if (currentPlayerInstance == null)
        {
            SpawnPlayer(); // Instancia o jogador se ainda não existir
        }
        else
        {
            RepositionPlayer(); // Reposiciona o jogador existente
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Remove o evento ao destruir o objeto
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (System.Array.Exists(fasesParaInstanciar, fase => fase == scene.name))
        {
            if (currentPlayerInstance != null)
            {
                RepositionPlayer(); // Apenas reposiciona se o jogador já existe
            }
            else
            {
                SpawnPlayer(); // Cria o jogador se ele não existe
            }
        }
    }

    private void SpawnPlayer()
    {
        currentPlayerInstance = Instantiate(playerPrefab, transform.position, Quaternion.identity); // Instancia o jogador
        DontDestroyOnLoad(currentPlayerInstance); // Garante que persista entre cenas
    }

    private void RepositionPlayer()
    {
        currentPlayerInstance.transform.position = transform.position; // Reposiciona na posição do spawn
    }
}
