using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCena : MonoBehaviour
{
    public string[] CenasDia; // Array com os nomes das cenas para onde o Player pode ser redirecionado
    public string[] CenasTarde;
    public string[] CenasNoite;

    int randomIndex;
    public static int faseatual = 15;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido é o Player ou alguma variante dele
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verifica se há cenas no array
            if (CenasDia.Length > 0)
            {
                {
                    // Escolhe uma cena aleatória do array
                    randomIndex = Random.Range(0, CenasDia.Length);
                }
                while(randomIndex == faseatual)

                faseatual = randomIndex;
                // Troca para a cena escolhida aleatoriamente
                SceneManager.LoadScene(CenasDia[randomIndex]);
                
            }
            else
            {
                Debug.LogWarning("Nenhuma cena disponível no array para troca.");
            }
        }

        if (collision.gameObject.CompareTag("Player_Grande"))
        {
            // Verifica se há cenas no array
            if (CenasTarde.Length > 0)
            {
                // Escolhe uma cena aleatória do array
                int randomIndex = Random.Range(0, CenasTarde.Length);

                // Troca para a cena escolhida aleatoriamente
                SceneManager.LoadScene(CenasDia[randomIndex]);
            }
            else
            {
                Debug.LogWarning("Nenhuma cena disponível no array para troca.");
            }
        }

        if (collision.gameObject.CompareTag("Player_Giga"))
        {
            // Verifica se há cenas no array
            if (CenasNoite.Length > 0)
            {
                // Escolhe uma cena aleatória do array
                int randomIndex = Random.Range(0, CenasNoite.Length);

                // Troca para a cena escolhida aleatoriamente
                SceneManager.LoadScene(CenasNoite[randomIndex]);
            }
            else
            {
                Debug.LogWarning("Nenhuma cena disponível no array para troca.");
            }
        }
            
    }
}
