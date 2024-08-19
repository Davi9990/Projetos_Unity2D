using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
   public Transform pontoA, pontoB;
    public int Speed;
    Vector2 targetPos;

    void Start()
    {
        targetPos = pontoB.position;
    }
 
    void Update()
    {
        if(Vector2.Distance(transform.position,pontoA.position) < .1f) targetPos = pontoB.position;

        if (Vector2.Distance(transform.position, pontoB.position) < .1f) targetPos = pontoA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
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
