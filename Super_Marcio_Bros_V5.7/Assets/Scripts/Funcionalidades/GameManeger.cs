using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private TextMeshProUGUI phaseText; // Referência ao TextMeshProUGUI para exibir a fase

    private int currentPhase = 1;
    private int currentSubPhase = 1;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject); // Certifique-se de que o GameManager não seja destruído ao trocar de cena
        UpdatePhaseText(); // Atualizar o texto no início do jogo
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeScene(string sceneName)
    {
        SavePlayerPosition();
        SceneManager.LoadScene(sceneName);
    }

    private void SavePlayerPosition()
    {
        if (playerTransform != null)
        {
            PlayerPrefs.SetFloat("PlayerPositionX", playerTransform.position.x);
            PlayerPrefs.SetFloat("PlayerPositionY", playerTransform.position.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", playerTransform.position.z);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RestorePlayerPosition();
        UpdatePhase(scene.name); // Atualizar a fase ao carregar uma nova cena
        UpdatePhaseText(); // Atualizar o texto da fase
    }

    private void RestorePlayerPosition()
    {
        if (playerTransform != null)
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX", playerTransform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerPositionY", playerTransform.position.y);
            float z = PlayerPrefs.GetFloat("PlayerPositionZ", playerTransform.position.z);
            playerTransform.position = new Vector3(x, y, z);
        }
    }

    private void UpdatePhase(string sceneName)
    {
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
            //currentPhase++;
        }
        else if (sceneName.Contains("Fase3"))
        {
            currentSubPhase = 3;
            //currentPhase++;
        }
        else if (sceneName.Contains("Fase4"))
        {
            currentSubPhase = 4;
            //currentPhase++;
        }
    }

    private void UpdatePhaseText()
    {
        if (phaseText != null)
        {
            phaseText.text = $"{currentPhase}-{currentSubPhase}";
        }
    }
}
