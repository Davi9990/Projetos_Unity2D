using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta_Butão : MonoBehaviour
{
    public GameObject Porta;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(Porta);
        }
    }


}
