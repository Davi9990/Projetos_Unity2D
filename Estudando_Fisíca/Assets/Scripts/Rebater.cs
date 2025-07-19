using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebater : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 impulso = rb.mass * rb.velocity;
        rb.AddForce(-impulso * 0.8f, ForceMode2D.Impulse);
        Debug.Log("Impulso de rebote aplicado: " + (-impulso * 0.8f));
    }
}
