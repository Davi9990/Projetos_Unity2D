using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cenas : MonoBehaviour
{

    public bool telatitulo;
    public string cenatitulo;

    private void Update()
    {
        if (telatitulo)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoadScenes(cenatitulo);
            }
        }
    }

    public void LoadScenes(string cenas)
    {
        SceneManager.LoadScene(cenas);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
