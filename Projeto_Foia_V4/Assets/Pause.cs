using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel; // Referência ao painel de pausa

    private bool isPaused = false;

    void Start()
    {
        // Garantir que o jogo comece não pausado
        ResumeGame();
    }

    void Update()
    {
        // Verificar se o jogador pressionou o botão de pausa
        if (Input.GetKeyDown(KeyCode.Escape)) // Pode ser o botão que você definir na HUD
        {
            if (isPaused)
                ResumeGame(); // Se já está pausado, retomar o jogo
            else
                PauseGame(); // Se não está pausado, pausar o jogo
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pausar o tempo do jogo
        pausePanel.SetActive(true); // Ativar o painel de pausa
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Retomar o tempo normal do jogo
        pausePanel.SetActive(false); // Desativar o painel de pausa
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Voltando para o menu inicial...");
        // Exemplo: SceneManager.LoadScene("MainMenu");
    }
}
