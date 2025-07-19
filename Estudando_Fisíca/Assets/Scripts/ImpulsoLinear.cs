using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsoLinear : MonoBehaviour
{
    public float intensidadeImpulso = 5f;
    public Vector2 direcao = Vector2.right;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(direcao.normalized * intensidadeImpulso, ForceMode2D.Impulse);
            Debug.Log("Impulso aplicado: " + direcao.normalized * intensidadeImpulso);
        }
    }
}
