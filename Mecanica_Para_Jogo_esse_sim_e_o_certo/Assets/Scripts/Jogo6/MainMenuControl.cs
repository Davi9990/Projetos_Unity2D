using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Pergunta1");
    }

    public void GotoTrophyRoom()
    {
        SceneManager.LoadScene("Sala_De_Trofeus");
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
