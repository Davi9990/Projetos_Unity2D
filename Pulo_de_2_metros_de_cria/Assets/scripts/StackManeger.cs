using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManeger : MonoBehaviour
{
    public Transform leftSpawn;
    public Transform rightSpawn;
    public float blockHeight = 1f;

    public void MoveSpawnPointsUp()
    {
        Vector3 up = new Vector3(0f, blockHeight, 0f);
        leftSpawn.position += up;
        rightSpawn.position += up;
    }
}
