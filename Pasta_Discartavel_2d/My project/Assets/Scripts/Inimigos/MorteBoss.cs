using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteBoss : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            // Destruir o boss ao colidir com a lava
            Destroy(gameObject);
        }
    }
}
