using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCena : MonoBehaviour
{
    public string sceneName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChageScene();
    }

    private void ChageScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
