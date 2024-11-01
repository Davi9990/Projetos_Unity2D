using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queimar : MonoBehaviour
{
    public BoxCollider2D dano;

    public SistemaDeVida vida;
    public int damage = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Collider>() == dano && collision.gameObject.tag == "Player")
        {

        }
    }
}
