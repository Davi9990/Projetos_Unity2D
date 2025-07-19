using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocadeMovimento : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float n = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(n * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rbA = GetComponent<Rigidbody2D>();
            Rigidbody2D rbB = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 momentoA = rbA.mass * rbA.velocity;
            Vector2 momentoB = rbB.mass * rbB.velocity;

            rbA.velocity = momentoB / rbA.mass;
            rbB.velocity = momentoA / rbB.mass;
        }
    }
}
