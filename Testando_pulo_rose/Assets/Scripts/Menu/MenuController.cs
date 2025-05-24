using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
