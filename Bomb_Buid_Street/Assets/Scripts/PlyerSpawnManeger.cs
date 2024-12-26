using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject player;  // Refer�ncia ao Player

    void Start()
    {
        // Verifica se existe uma posi��o salva
        if (PlayerPrefs.HasKey("PlayerSpawnX") && PlayerPrefs.HasKey("PlayerSpawnY") && PlayerPrefs.HasKey("PlayerSpawnZ"))
        {
            // Recupera as coordenadas salvas
            float x = PlayerPrefs.GetFloat("PlayerSpawnX");
            float y = PlayerPrefs.GetFloat("PlayerSpawnY");
            float z = PlayerPrefs.GetFloat("PlayerSpawnZ");

            // Define a nova posi��o para o Player
            player.transform.position = new Vector3(x, y, z);
        }
    }
}
