using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gerador_de_Itend_e_Obstaculos : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerUpPrefabs;
    public Transform player;

    private float spawnZ = 0f;
    private float tileLength = 30f;
    private int tilesOnScreen = 5;

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile(i < 2 ? 0 : Random.Range(0, obstaclePrefabs.Length));
        }
    }

    void Update()
    {
        if (player.position.z - 30f > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile(Random.Range(0, obstaclePrefabs.Length));
        }
    }

    private void SpawnTile(int prefabIndex)
    {
        GameObject go = Instantiate(obstaclePrefabs[prefabIndex], Vector3.forward * spawnZ, Quaternion.identity);
        spawnZ += tileLength;
    }
}
