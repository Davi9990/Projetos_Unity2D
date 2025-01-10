using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCena : MonoBehaviour
{
    public string[] sceneNames; // Array com os nomes das cenas para onde o Player pode ser redirecionado

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido � o Player ou alguma variante dele
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player_Grande") ||
            collision.gameObject.CompareTag("Player_Giga"))
        {
            // Verifica se h� cenas no array
            if (sceneNames.Length > 0)
            {
                // Escolhe uma cena aleat�ria do array
                int randomIndex = Random.Range(0, sceneNames.Length);

                // Troca para a cena escolhida aleatoriamente
                SceneManager.LoadScene(sceneNames[randomIndex]);
            }
            else
            {
                Debug.LogWarning("Nenhuma cena dispon�vel no array para troca.");
            }
        }
    }
}
