using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trocando_Fase : MonoBehaviour
{
    public void TrocaDeCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }
}
