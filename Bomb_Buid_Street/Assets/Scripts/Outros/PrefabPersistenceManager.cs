using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public GameObject[] playerPrefabs; // Array com os prefabs do Player
    private GameObject activePlayer; // Prefab ativo
    public Vector3 spawnPosition; // Posição para instanciar na próxima cena

    void Awake()
    {
        // Garantir que apenas um PlayerManager exista
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Inicializar os prefabs
        foreach (GameObject prefab in playerPrefabs)
        {
            if (prefab != null)
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                prefab.SetActive(false); // Desativa todos no início
            }
        }

        // Ativar o primeiro prefab como padrão
        if (playerPrefabs.Length > 0)
        {
            activePlayer = playerPrefabs[0];
            activePlayer.SetActive(true);
        }

        // Ouvir evento de troca de cena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reposicionar o Player na posição de reaparecimento
        if (activePlayer != null)
        {
            activePlayer.transform.position = spawnPosition;
        }
    }

    public void SetSpawnPosition(Vector3 newPosition)
    {
        spawnPosition = newPosition;
    }

    public void SwapPrefab(int prefabIndex)
    {
        if (prefabIndex < 0 || prefabIndex >= playerPrefabs.Length)
            return;

        // Desativar o prefab atual
        if (activePlayer != null)
        {
            activePlayer.SetActive(false);
        }

        // Ativar o novo prefab
        activePlayer = playerPrefabs[prefabIndex];
        if (activePlayer != null)
        {
            activePlayer.transform.position = spawnPosition;
            activePlayer.SetActive(true);
        }
    }
}
