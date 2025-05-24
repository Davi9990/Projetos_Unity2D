using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform leftSpawn;
    public Transform rightSpawn;

    private bool spawnFromLeft = true;

    public void SpawnBlock()
    {
        Transform spawnPoint = spawnFromLeft ? leftSpawn : rightSpawn;
        Vector2 direction = spawnFromLeft ? Vector2.right : Vector2.left;

        GameObject block = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
        spawnFromLeft = !spawnFromLeft;
    }
}
