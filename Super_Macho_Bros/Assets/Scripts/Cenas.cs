using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cenas : MonoBehaviour
{
   
    public void LoadScenes(string cenas)
    {
        SceneManager.LoadScene(cenas);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
