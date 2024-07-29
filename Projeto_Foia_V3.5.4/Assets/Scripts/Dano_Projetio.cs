using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano_Projetio : MonoBehaviour
{
    public int danoInimigo = 25;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Barra_de_Vida>().maxHealth -= danoInimigo;
            Destroy(gameObject);
        }
    }

}
