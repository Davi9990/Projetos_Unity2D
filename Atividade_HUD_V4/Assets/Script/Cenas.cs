using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cenas : MonoBehaviour
{
    private const string LastSceneKey = "LastScene";

    // Fun��o para carregar uma nova cena
    public void LoadScene(string sceneName)
    {
        // Armazena o nome da cena atual antes de mudar para a nova cena
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Armazenando cena atual: " + currentSceneName);
        PlayerPrefs.SetString(LastSceneKey, currentSceneName);
        PlayerPrefs.Save();

        // Carrega a nova cena
        Debug.Log("Carregando nova cena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    // Fun��o para recome�ar a �ltima cena em que o jogador estava
    public void RestartLastScene()
    {
        // Recupera o nome da �ltima cena armazenada
        string lastScene = PlayerPrefs.GetString(LastSceneKey, null);
        if (!string.IsNullOrEmpty(lastScene))
        {
            Debug.Log("Reiniciando cena: " + lastScene);
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.Log("Nenhuma cena armazenada encontrada. Verifique se a fun��o LoadScene foi chamada corretamente.");
        }
    }

    // Fun��o para inicializar o jogo e carregar a cena de menu principal
    public void LoadMenu()
    {
        Debug.Log("Carregando Menu Principal");
        SceneManager.LoadScene("MenuPrincipal");
    }
}
