using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov_Damage : MonoBehaviour
{
    public int dano = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Atingido pela Molotov! - Dano: " + dano);

            Destroy(gameObject);    
        }
    }
}
