using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    private void Start()
    {
        // Verifica se as coordenadas de spawn foram salvas
        if (PlayerPrefs.HasKey("PlayerSpawnX") && PlayerPrefs.HasKey("PlayerSpawnY") && PlayerPrefs.HasKey("PlayerSpawnZ"))
        {
            // Lê as coordenadas salvas e reposiciona o Player
            float x = PlayerPrefs.GetFloat("PlayerSpawnX");
            float y = PlayerPrefs.GetFloat("PlayerSpawnY");
            float z = PlayerPrefs.GetFloat("PlayerSpawnZ");

            // Reposiciona o player para as coordenadas salvas
            transform.position = new Vector3(x, y, z);
            Debug.Log($"Reposicionando para: X={x}, Y={y}, Z={z}");
        }
        else
        {
            // Se não houver coordenadas salvas, o player não será reposicionado
            Debug.LogWarning("Nenhum ponto de spawn foi encontrado. O Player permanecerá na posição inicial.");
        }
    }
}
