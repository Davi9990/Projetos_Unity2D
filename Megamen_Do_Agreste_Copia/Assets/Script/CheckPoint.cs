using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint registrado!");
            FindObjectOfType<PlayerSpawner>().RegistrarCheckpoint(transform.position);
        }
    }
}
