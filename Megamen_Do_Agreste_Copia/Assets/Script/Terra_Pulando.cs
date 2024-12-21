using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra_Pulando : MonoBehaviour
{
    private Rigidbody2D rb;
    public float JumpForce = 5f;
    public Transform Player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pulo"))
        {
            Vector2 JumpDirection = (Player.position - transform.position).normalized;

            rb.velocity = new Vector2(JumpDirection.x, 1) * JumpForce;
        }
    }
}
