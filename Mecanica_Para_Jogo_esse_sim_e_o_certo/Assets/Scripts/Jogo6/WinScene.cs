using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GotoMainMenu", 1f);
    }

    void GotoMainMenu()
    {
        SceneManager.LoadScene("Jogo6");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
