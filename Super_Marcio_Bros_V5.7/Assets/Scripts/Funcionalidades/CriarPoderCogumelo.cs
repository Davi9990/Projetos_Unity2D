using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarPoderCogumelo : MonoBehaviour
{
    public GameObject upgradedPrefab; // Prefab do player com poder

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPowerManeger powerManager = collision.GetComponent<PlayerPowerManeger>();
            if (powerManager != null)
            {
                powerManager.UpgradePlayer(upgradedPrefab);
                Destroy(gameObject); // Destrói o cogumelo após pegar
            }
        }
    }
}
