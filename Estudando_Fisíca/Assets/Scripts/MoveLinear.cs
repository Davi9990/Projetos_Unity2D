using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLinear : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 movimentoLinear = rb.mass * rb.velocity;
       Debug.Log("Movimento Linear: " + movimentoLinear);
    }
}
