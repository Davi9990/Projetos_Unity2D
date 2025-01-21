using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab do jogador
    public string[] fasesParaInstanciar; // Nomes das fases onde o jogador deve ser reposicionado
    public int vidasMaximas = 3; // N�mero m�ximo de vidas
    private int vidasAtuais; // Vidas atuais do jogador
    private static GameObject currentPlayerInstance; // Refer�ncia �nica ao jogador
    private static Vector3 checkpointAtual; // Posi��o do �ltimo checkpoint
    private static Vector3 spawnInicial; // Posi��o inicial do spawn

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Conecta o m�todo ao evento de cena carregada

        if (currentPlayerInstance == null)
        {
            vidasAtuais = vidasMaximas; // Inicializa as vidas apenas na primeira inst�ncia
            spawnInicial = transform.position; // Define o spawn inicial como o primeiro ponto
            checkpointAtual = spawnInicial; // Inicializa o checkpoint com a posi��o inicial
            SpawnPlayer(); // Instancia o jogador
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
                RepositionPlayer(); // Apenas reposiciona se o jogador j� existe
            }
            else
            {
                SpawnPlayer(); // Cria o jogador se ele n�o existe
            }
        }
    }

    private void SpawnPlayer()
    {
        currentPlayerInstance = Instantiate(playerPrefab, checkpointAtual, Quaternion.identity); // Instancia o jogador
        DontDestroyOnLoad(currentPlayerInstance); // Garante que o jogador persista entre cenas
    }

    private void RepositionPlayer()
    {
        currentPlayerInstance.transform.position = checkpointAtual; // Reposiciona o jogador no �ltimo checkpoint
    }

    public void RegistrarCheckpoint(Vector3 novaPosicao)
    {
        checkpointAtual = novaPosicao; // Atualiza o checkpoint atual
        Debug.Log($"Checkpoint registrado: {novaPosicao}");
    }

    public void PlayerMorreu()
    {
        vidasAtuais--;

        if (vidasAtuais <= 0)
        {
            GameOver();
        }
        else
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        if (currentPlayerInstance != null)
        {
            Destroy(currentPlayerInstance); // Garante que n�o h� outra inst�ncia do jogador
        }

        // Respawna no �ltimo checkpoint ou no spawn inicial
        checkpointAtual = checkpointAtual == Vector3.zero ? spawnInicial : checkpointAtual;
        SpawnPlayer();
    }

    private void GameOver()
    {
        vidasAtuais = vidasMaximas; // Reseta as vidas
        SceneManager.LoadScene("SelecaoDeFase"); // Carrega a cena de Game Over
    }
}
