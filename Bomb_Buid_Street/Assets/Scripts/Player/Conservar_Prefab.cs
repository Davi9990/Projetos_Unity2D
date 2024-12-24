using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Conservar_Prefab : MonoBehaviour
{
    private void Awake()
    {
        // Registra o objeto no PrefabPersistenceManager
        //PrefabPersistenceManager.Instance.RegisterPrefab(gameObject);
    }

    private void OnDestroy()
    {
        // Remove o objeto da lista ao ser destruído
        //PrefabPersistenceManager.Instance.UnregisterPrefab(gameObject);
    }
}
