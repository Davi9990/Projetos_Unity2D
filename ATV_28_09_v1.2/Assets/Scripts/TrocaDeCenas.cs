using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCenas : MonoBehaviour
{
   public void LoadScene(string cenas)
    {
        SceneManager.LoadScene(cenas);
    }
}
