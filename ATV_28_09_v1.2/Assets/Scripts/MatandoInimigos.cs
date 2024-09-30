using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatandoInimigos : MonoBehaviour
{
    public GameObject Enemy;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(Enemy);
        }
    }
}
