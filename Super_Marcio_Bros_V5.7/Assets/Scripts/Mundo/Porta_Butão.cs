using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta_Butão : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Machado;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(Enemy);
            Destroy(Machado); 
        }
    }
}
