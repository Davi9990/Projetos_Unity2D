using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    //public Transform pontoA, pontoB;
    public bool desce;
    private int Speed;
    float temp;
    Vector2 targetPos;

    void Start()
    {
        //targetPos = pontoB.position;
        if( desce)
        {
            Speed = -5;
        }
        else
        {
            Speed = 5;
        }
        temp = 8;
    }
 
    void Update()
    {
        /*if(Vector2.Distance(transform.position,pontoA.position) < .1f) targetPos = pontoB.position;

        if (Vector2.Distance(transform.position, pontoB.position) < .1f) targetPos = pontoA.position;*/

        temp -= Time.deltaTime;
        if (temp < 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(new Vector2(0, Speed * Time.deltaTime));
        /*
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
