using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class Move : MonoBehaviour
{
    //Movimentação
    public float velocidade;
    private Rigidbody2D rb;

    //Pulos 2 Metros
    public float velocidadePulo = 6.3f;
    public bool estaNoChao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moves();

        if(Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocidadePulo);
            estaNoChao = false;
        }
    }

    private void Moves()
    {
        float xAxis = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Chao"))
        {
            estaNoChao = true;

            
            FindObjectOfType<BlockSpawner>().SpawnBlock();
        }

        if (other.collider.CompareTag("Morte"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
