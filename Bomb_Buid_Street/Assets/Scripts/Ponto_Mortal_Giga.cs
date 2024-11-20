using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ponto_Mortal_Giga : MonoBehaviour
{
    public GameObject Inimigo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player_Giga"))
        {
            Destroy(Inimigo);
        }
    }
}
