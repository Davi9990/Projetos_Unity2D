using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCena : MonoBehaviour
{
    public string sceneName; // Nome da cena para onde o Player será redirecionado

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido é o Player
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player_Grande") ||
            collision.gameObject.CompareTag("Player_Giga"))
        {
            // Troca de cena
            SceneManager.LoadScene(sceneName);
        }
    }
}
