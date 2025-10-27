using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject groundPrefab;
    public int poolSize = 8;
    public float segmentLength = 30f;
    public int spawnObstaclesPerSegment = 3;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public float lateralLimit = 4f;

    Queue<GameObject> pool = new Queue<GameObject>();
    float spawnZ = 0f;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(groundPrefab, Vector3.forward * (spawnZ + i * segmentLength), Quaternion.identity);
            pool.Enqueue(go);
        }
        spawnZ = poolSize * segmentLength;
    }

    void Update()
    {
        if (player.position.z + (poolSize - 2) * segmentLength > spawnZ - poolSize * segmentLength)
        {
            RecycleAndSpawn();
        }
    }

    void RecycleAndSpawn()
    {
        GameObject old = pool.Dequeue();
        old.transform.position = new Vector3(0f, 0f, spawnZ);
        ClearChildren(old);
        SpawnObstaclesAndCoins(old);
        pool.Enqueue(old);
        spawnZ += segmentLength;
    }

    void ClearChildren(GameObject segment)
    {
        // Remove previmente os objetos 
        for (int i = segment.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(segment.transform.GetChild(i).gameObject);
        }
    }

    void SpawnObstaclesAndCoins(GameObject segment)
    {
        // Spawna itens de maneira aleatorioa
        for (int i = 0; i < spawnObstaclesPerSegment; i++)
        {
            if (obstaclePrefabs.Length == 0) break;
            Vector3 pos = new Vector3(Random.Range(-lateralLimit, lateralLimit), 0.6f, segment.transform.position.z + Random.Range(2f, segmentLength - 2f));
            GameObject c = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], pos, Quaternion.identity, segment.transform);
            c.tag = "Obstacle";
        }

        // Range de spawnwer
        int coinCount = Random.Range(3, 7);
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-lateralLimit, lateralLimit), 0.5f, segment.transform.position.z + Random.Range(1f, segmentLength - 1f));
            GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity, segment.transform);
            coin.tag = "Coin";
        }
    }
}
