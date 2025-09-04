using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRU : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direcao = new Vector2(Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical"));

        rb.velocity = direcao * velocidade;

        if (direcao.x != 0 && direcao.y != 0) direcao = direcao.normalized;
            
    }
}
