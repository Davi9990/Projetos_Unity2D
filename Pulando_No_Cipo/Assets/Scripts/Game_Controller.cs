using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    [Header("Configurações do Jogo")]
    public List<RopeController> ropes;   // Lista de cordas (definidas na ordem de travessia)
    public float totalTime = 60f;        // Tempo total para concluir o desafio
    public int ropesToWin = 5;           // Quantidade de cordas necessárias para vencer

    [Header("Referências de UI")]
    public Text timerText;               // Texto para exibir o tempo restante
    public Text messageText;             // Texto para exibir mensagens (ex.: "Você caiu!")

    [Header("Referência do Jogador")]
    public Player_Controller player;

    private int ropesGrabbed = 0;        // Contador de cordas agarradas
    private bool gameOver = false;

    void Start()
    {
        if (ropes.Count > 0)
        {
            SetInitialSetup();
        }
        else
        {
            Debug.LogError("Nenhuma corda foi atribuída no GameManager!");
        }
        messageText.text = "";
    }

    void Update()
    {
        if (gameOver)
            return;

        // Atualiza o tempo
        totalTime -= Time.deltaTime;
        timerText.text = "Tempo: " + Mathf.Round(totalTime).ToString();

        if (totalTime <= 0)
        {
            GameOver("Tempo esgotado!");
        }
    }

    void SetInitialSetup()
    {
        // Todas as cordas já estarão visíveis no início
        foreach (var rope in ropes)
        {
            rope.gameObject.SetActive(true);
        }
    }

    // Chamada quando o jogador agarra uma nova corda
    public void RopeGrabbed()
    {
        if (gameOver) return;

        ropesGrabbed++;

        // Verifica se o jogador atingiu o objetivo
        if (ropesGrabbed >= ropesToWin)
        {
            Victory();
        }
    }

    public void GameOver(string message)
    {
        gameOver = true;
        messageText.text = message;
        player.enabled = false;  // Impede que o jogador continue se movimentando
        SceneManager.LoadScene("Tela_De_Titulo");
    }

    void Victory()
    {
        gameOver = true;
        messageText.text = "Você Venceu!";
        player.enabled = false;
        SceneManager.LoadScene("Tela_De_Titulo");
    }
}
