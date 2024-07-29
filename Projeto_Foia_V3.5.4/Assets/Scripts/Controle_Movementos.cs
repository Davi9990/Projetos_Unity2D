using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle_Movementos : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;
    public bool chao1;
    public Transform detectarchao;
    public LayerMask chao0;
    public int pulos = 2;
    public Rigidbody2D Player;
   

    [Header("Coyote Time Settings")]
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimer = 0f;
    private bool isOnGround = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis,rb.velocity.y);
    }

    void CoyoteTimeCheck()
    {
        if (isOnGround)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }
  
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && coyoteTimer > 0)
        {
            chao1 = Physics2D.OverlapCircle(detectarchao.position, 0.1f, chao0);

            if (Input.GetButtonDown("Jump") && pulos > 0)
            {
                Player.velocity = Vector2.up * 15;
                pulos--;
            }
            if (chao1)
            {
                pulos = 1;
            }
            Debug.Log("Jump!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
