using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManeger : MonoBehaviour
{
    public int vidasMaximas = 3; // Número máximo de vidas
    public int vidasAtuais; // Vidas atuais do jogador
    public Vector3 checkpointAtual; // Posição do último checkpoint
    public GameObject playerPrefab; // Prefab do jogador
    private static GameObject jogadorAtual; // Referência única ao jogador

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Preserva este objeto entre cenas
        if (jogadorAtual == null) // Garante que só existe uma instância do jogador
        {
            SpawnPlayer();
        }
        else
        {
            ReposicionarJogador();
        }
    }

    private void Start()
    {
        vidasAtuais = vidasMaximas; // Inicializa as vidas
        checkpointAtual = transform.position; // Define o ponto inicial como o primeiro checkpoint
    }

    public void RegistrarCheckpoint(Vector3 novaPosicao)
    {
        checkpointAtual = novaPosicao; // Atualiza o checkpoint
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

    private void GameOver()
    {
        vidasAtuais = vidasMaximas; // Reseta as vidas
        SceneManager.LoadScene("SelecaoDeFase"); // Carrega a cena de Game Over
    }

    private void SpawnPlayer()
    {
        jogadorAtual = Instantiate(playerPrefab, checkpointAtual, Quaternion.identity); // Cria o jogador
        DontDestroyOnLoad(jogadorAtual); // Preserva o jogador entre cenas
    }

    private void RespawnPlayer()
    {
        if (jogadorAtual != null)
        {
            jogadorAtual.transform.position = checkpointAtual; // Reposiciona o jogador no último checkpoint
        }
    }

    private void ReposicionarJogador()
    {
        // Reposiciona o jogador atual para o checkpoint caso ele já exista
        if (jogadorAtual != null)
        {
            jogadorAtual.transform.position = checkpointAtual;
        }
    }
}
