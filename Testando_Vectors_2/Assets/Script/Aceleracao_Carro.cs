using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aceleracao_Carro : MonoBehaviour
{
    public float velocidade_inicial;
    private float velocidade_final = 7;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), transform.position.y);

        Vector2 posicaofinal = new Vector3();
    }
}
