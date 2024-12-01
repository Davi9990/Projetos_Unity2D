using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab do jogador
    public string[] fasesParaInstanciar; // Nomes das fases onde o jogador deve ser instanciado
    private GameObject currentPlayerInstance; // Refer�ncia �nica ao jogador

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica se a cena atual � uma das fases especificadas no array
        if (System.Array.Exists(fasesParaInstanciar, fase => fase == scene.name))
        {
            if (currentPlayerInstance == null)
            {
                SpawnPlayer();
            }
            else
            {
                // Atualiza a posi��o do jogador para o ponto de spawn
                currentPlayerInstance.transform.position = transform.position;
            }
        }
        else
        {
            // Destroi o jogador se n�o estiver em uma das fases do array
            if (currentPlayerInstance != null)
            {
                Destroy(currentPlayerInstance);
                currentPlayerInstance = null;
            }
        }
    }

    private void SpawnPlayer()
    {
        currentPlayerInstance = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(currentPlayerInstance);
    }
}
