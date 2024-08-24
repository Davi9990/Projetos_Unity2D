using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
   
   public void LoadScenes(string Cenas)
    {
        SceneManager.LoadScene(Cenas);
    }
}
