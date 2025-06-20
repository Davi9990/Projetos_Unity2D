using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCena : MonoBehaviour
{
    public string[] cena;

    int randomIndex;
    public static int faseatual = 15;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(cena.Length > 0)
            {
                randomIndex = Random.Range(0, cena.Length);
            }

            while(randomIndex == faseatual)
            
            faseatual = randomIndex;

            SceneManager.LoadScene(cena[randomIndex]);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena disponível no array para troca.");
        }
    }
}
