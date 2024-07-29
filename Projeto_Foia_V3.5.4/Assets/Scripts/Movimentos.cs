using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentos : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D corpo;


    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * velocidade;
    }
}
