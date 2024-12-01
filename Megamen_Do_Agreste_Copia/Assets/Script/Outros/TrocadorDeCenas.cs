using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocadorDeCenas : MonoBehaviour
{
    public GameObject fasef;

    public bool Iara = false, Boitata = false, Curupira = false;

    private void Start()
    {
        if (Iara && Boitata && Curupira)
        {
            fasef.SetActive(true);
        }
    }

    public void Trocar(string nome)
    {
        SceneManager.LoadScene(nome);
    }
}
