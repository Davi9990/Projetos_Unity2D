using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Condicao_De_Vitoria : MonoBehaviour
{
    public Cronometro cronos; // Refer�ncia ao cron�metro
    public TextMeshProUGUI Text; // Texto da tela de vit�ria
    public GameObject Player1; // Refer�ncia ao Player 1
    public GameObject Player2; // Refer�ncia ao Player 2

    void Start()
    {
        // Verifica se o cron�metro foi atribu�do no Inspector
        if (cronos == null)
        {
            Debug.LogError("Cron�metro n�o atribu�do no Inspector!");
        }

        // Garante que Player1 e Player2 estejam atribu�dos
        if (Player1 == null || Player2 == null)
        {
            Debug.LogError("Jogadores n�o atribu�dos no Inspector!");
        }

        // Garante que o Text foi atribu�do no Inspector
        if (Text == null)
        {
            Debug.LogError("Texto n�o atribu�do no Inspector!");
        }
    }

    void Update()
    {
        // Verifica se o cron�metro parou
        if (!cronos.timerIsRunning)
        {
            VerificarVencedor();
        }
    }

    void VerificarVencedor()
    {
        // Atualiza as Tags para garantir consist�ncia
        string tagPlayer1 = Player1.tag;
        string tagPlayer2 = Player2.tag;

        Debug.Log($"Tag Player1: {tagPlayer1} | Tag Player2: {tagPlayer2}");

        // Determina o vencedor com base nas Tags
        if (tagPlayer1 == "Player_Fugitivo")
        {
            Text.text = "Player 1 Ganhou!";
        }
        else if (tagPlayer2 == "Player_Fugitivo")
        {
            Text.text = "Player 2 Ganhou!";
        }
        else
        {
            Text.text = "Erro: Nenhum vencedor detectado.";
            Debug.LogError("Nenhum jogador est� com a Tag 'Player_Fugitivo'. Verifique a l�gica de troca de Tags.");
        }
    }
}
