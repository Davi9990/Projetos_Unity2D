using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Condicao_De_Vitoria : MonoBehaviour
{
    public Cronometro cronos; // Referência ao cronômetro
    public TextMeshProUGUI Text; // Texto da tela de vitória
    public GameObject Player1; // Referência ao Player 1
    public GameObject Player2; // Referência ao Player 2

    void Start()
    {
        // Verifica se o cronômetro foi atribuído no Inspector
        if (cronos == null)
        {
            Debug.LogError("Cronômetro não atribuído no Inspector!");
        }

        // Garante que Player1 e Player2 estejam atribuídos
        if (Player1 == null || Player2 == null)
        {
            Debug.LogError("Jogadores não atribuídos no Inspector!");
        }

        // Garante que o Text foi atribuído no Inspector
        if (Text == null)
        {
            Debug.LogError("Texto não atribuído no Inspector!");
        }
    }

    void Update()
    {
        // Verifica se o cronômetro parou
        if (!cronos.timerIsRunning)
        {
            VerificarVencedor();
        }
    }

    void VerificarVencedor()
    {
        // Atualiza as Tags para garantir consistência
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
            Debug.LogError("Nenhum jogador está com a Tag 'Player_Fugitivo'. Verifique a lógica de troca de Tags.");
        }
    }
}
