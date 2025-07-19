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
            if(cena.Length == 0)
            {
                Debug.LogWarning("Nenhuma cena disponível no array para troca");
                return;
            }

            int tentativa = 0;

            do
            {
                randomIndex = Random.Range(0, cena.Length);
                tentativa++;


                // Se não achar outra cenas depois de 100 tenativas, quebra o Loop por segurança
                if(tentativa > 100)
                {
                    Debug.LogError("Loop de seleção de cena excedeu limite de tentativas");
                    break;
                }
            } while (randomIndex == faseatual && cena.Length > 1);

            faseatual = randomIndex;

            SceneManager.LoadScene(cena[randomIndex]);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena disponível no array para troca.");
        }
    }
}
