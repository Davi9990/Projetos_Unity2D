using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Movimentação
    float dirX;
    float dirY;
    public float moveSpeed = 10f;
    public bool facingRight = true;

    //Pulos
    public float JumpForce;
    private bool EstaNoChao = false;
    private Rigidbody2D rb;
    public int Pulos = 2;
    private int PulosDisponiveis;

    //Atirar
    public Transform Hand;
    public GameObject Balas;
    public float speedBullet;
    public float fireRate = 1f;
    public float nextFireTime = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        //dirY = Input.GetAxis("Vertical");

        transform.position = new Vector2(transform.position.x + dirX * moveSpeed * Time.deltaTime,
            transform.position.y);

        if(dirX > 0 && !facingRight)
        {
            virarPersonagem();
        }
        else if(dirX < 0  && facingRight)
        {
            virarPersonagem();
        }

        Pular();
        Atirar();
    }

    void Pular()
    {
        if (Pulos > 0)
        {
            if (Input.GetButtonDown("Jump") && EstaNoChao == true)
            {
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                Pulos --;
                //EstaNoChao = false;
            }
        }
        else
        {
            EstaNoChao = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Chao")
        {
            EstaNoChao = true;
            Pulos = 2;
        }
    }

    void virarPersonagem()
    {
        facingRight = !facingRight;
        Vector3 scale = GetComponent<Transform>().localScale;
        scale.x *= -1;
        GetComponent<Transform>().localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pulo"))
        {
            rb.AddForce(Vector2.up * 300f);
        }
    }

    private void Atirar()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            GameObject newFire = Instantiate(Balas, Hand.position, Quaternion.identity);
            Rigidbody2D firerb = newFire.GetComponent<Rigidbody2D>();

            float direction =  facingRight ? 1 : -1;

            float bulletSpeed = speedBullet + Mathf.Abs(rb.velocity.x);
            firerb.velocity = new Vector2(direction * bulletSpeed, 0);

            //firerb.velocity = new Vector2(direction * speedBullet, 0);

            Destroy(newFire, 6f);
            nextFireTime = Time.time + fireRate;
        }
    }
}
