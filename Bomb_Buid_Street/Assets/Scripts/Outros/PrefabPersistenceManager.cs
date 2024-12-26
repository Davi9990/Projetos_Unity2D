using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefabPersistenceManager : MonoBehaviour
{
     public static PrefabPersistenceManager Instance; // Singleton para garantir uma instância
    private List<GameObject> prefabsToPersist = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adiciona um prefab à lista de persistência
    public void RegisterPrefab(GameObject prefab)
    {
        if (prefab != null && !prefabsToPersist.Contains(prefab))
        {
            prefabsToPersist.Add(prefab);
            DontDestroyOnLoad(prefab);
        }
    }

    // Remove um prefab da lista de persistência
    public void UnregisterPrefab(GameObject prefab)
    {
        if (prefabsToPersist.Contains(prefab))
        {
            prefabsToPersist.Remove(prefab);
        }
    }

    // Preserva todos os objetos da lista ao carregar uma nova cena
    public void PersistAllPrefabs()
    {
        foreach (var prefab in prefabsToPersist)
        {
            if (prefab != null)
            {
                DontDestroyOnLoad(prefab);
            }
        }
    }
}
