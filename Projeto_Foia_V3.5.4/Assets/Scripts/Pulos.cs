using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulos : MonoBehaviour
{
    public Rigidbody2D Player;
    public bool chao1;
    public Transform detectarchao;
    public LayerMask chao0;
    public int pulos = 2;
    

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        Player.gravityScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        chao1 = Physics2D.OverlapCircle(detectarchao.position, 0.1f, chao0);

        if(Input.GetButtonDown("Jump") && pulos > 0)
        {
            Player.velocity = Vector2.up * 20;
            pulos--;
        }
        if (chao1)
        {
            pulos = 1;
        }

        if (!chao1 && Player.velocity.y < 0)
        {
            Player.velocity += Vector2.up * Physics2D.gravity.y * (1.5f - 1) * Time.deltaTime;
        }
    }
}
