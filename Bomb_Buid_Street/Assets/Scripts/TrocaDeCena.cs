using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCena : MonoBehaviour
{
    public string sceneName;  // Nome da cena para carregar
    public Transform spawnPoint;  // Ponto de spawn para o Player na nova cena

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player_Grande") || collision.gameObject.CompareTag("Player_Giga"))
        {
            // Salva a posição de spawn
            PlayerPrefs.SetFloat("PlayerSpawnX", spawnPoint.position.x);
            PlayerPrefs.SetFloat("PlayerSpawnY", spawnPoint.position.y);
            PlayerPrefs.SetFloat("PlayerSpawnZ", spawnPoint.position.z);

            // Troca de cena
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
