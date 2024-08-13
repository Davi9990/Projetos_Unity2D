using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    public Transform playerTransform; // Referência ao transform do jogador
    [SerializeField] private TextMeshProUGUI phaseText; // Referência ao TextMeshProUGUI para exibir a fase

    private int currentPhase = 1; // Fase atual do jogo
    private int currentSubPhase = 1; // Subfase atual do jogo

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Adiciona um listener para o evento de carregamento de cena
        DontDestroyOnLoad(gameObject); // Garante que o GameManager não seja destruído ao trocar de cena
        UpdatePhaseText(); // Atualiza o texto da fase no início do jogo
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Remove o listener para evitar chamadas adicionais quando o GameManager for destruído
    }

    public void ChangeScene(string sceneName)
    {
        SavePlayerPosition(); // Salva a posição do jogador antes de trocar de cena
        SceneManager.LoadScene(sceneName); // Carrega a nova cena
    }

    private void SavePlayerPosition()
    {
        if (playerTransform != null)
        {
            // Salva a posição do jogador usando PlayerPrefs
            PlayerPrefs.SetFloat("PlayerPositionX", playerTransform.position.x);
            PlayerPrefs.SetFloat("PlayerPositionY", playerTransform.position.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", playerTransform.position.z);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RestorePlayerPosition(); // Restaura a posição do jogador ao carregar a nova cena
        UpdatePhase(scene.name); // Atualiza a fase com base no nome da cena
        UpdatePhaseText(); // Atualiza o texto da fase
    }

    private void RestorePlayerPosition()
    {
        if (playerTransform != null)
        {
            // Restaura a posição do jogador a partir dos valores salvos
            float x = PlayerPrefs.GetFloat("PlayerPositionX", playerTransform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerPositionY", playerTransform.position.y);
            float z = PlayerPrefs.GetFloat("PlayerPositionZ", playerTransform.position.z);
            playerTransform.position = new Vector3(x, y, z);
        }
    }

    private void UpdatePhase(string sceneName)
    {
        // Atualiza a fase e subfase com base no nome da cena
        if (sceneName.Contains("Pre_Fase_"))
        {
            currentSubPhase = 1;
        }
        else if (sceneName.Contains("Fase2"))
        {
            currentSubPhase = 2;
        }
        else if (sceneName.Contains("Pos_Fase"))
        {
            currentSubPhase = 2;
            //currentPhase++; // Incrementa a fase, se desejado
        }
        else if (sceneName.Contains("Fase3"))
        {
            currentSubPhase = 3;
            //currentPhase++; // Incrementa a fase, se desejado
        }
        else if (sceneName.Contains("Fase4"))
        {
            currentSubPhase = 4;
            //currentPhase++; // Incrementa a fase, se desejado
        }
    }

    private void UpdatePhaseText()
    {
        // Atualiza o texto da fase no TextMeshProUGUI
        if (phaseText != null)
        {
            phaseText.text = $"{currentPhase}-{currentSubPhase}";
        }
    }
}
