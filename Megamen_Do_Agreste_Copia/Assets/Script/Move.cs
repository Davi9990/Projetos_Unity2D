using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Movimenta��o
    public float speed;
    private Rigidbody2D rg;
    public bool isFacingRight = true;

    //Pulos
    public float JP;
    private bool CnJmp;
    private float tempinho = 0.5f;

    //Atirar
    public Transform Hand;
    public GameObject Balas;
    public float speedBulllets;
    public float fireRate = 1f;
    public float nextFireTime = 0f;

    SpriteRenderer visu;
    public static bool virou = false;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        visu = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        m();
        pular();
        Atirar();

        if (tempinho <= 0)
        {
            CnJmp = false;
            Debug.Log("O pulo brochou");
        }
    }

    void m()
    {
        float n = Input.GetAxisRaw("Horizontal");
        rg.velocity = new Vector2(n * speed, rg.velocity.y);

        //Verifica a dire��o do movimento e inverte o sprite se necessario

        if (n < 0)
        {
            isFacingRight = true;
            visu.flipX = true;
            virou = true;
        }
        else if (n > 0)
        {
            isFacingRight = false;
            visu.flipX = false;
            virou = false;
        }
    }

    void pular()
    {
        if (Input.GetButtonDown("Jump") && CnJmp == true)
        {
            rg.AddForce(new Vector2(0, JP), ForceMode2D.Impulse);
            CnJmp = false;
        }

        // Pulo vari�vel: reduz a velocidade do pulo se o jogador soltar o bot�o antes do pulo atingir o ponto m�ximo
        if (Input.GetButtonUp("Jump") && rg.velocity.y > 0)
        {
            rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y * 0.5f);
        }
    }

    private void Atirar()
    {
        if(Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            GameObject newFire = Instantiate(Balas, Hand.position, Quaternion.identity);
            // Acessa o Rigidbody2D do proj�til instanciado
            Rigidbody2D firerb = newFire.GetComponent<Rigidbody2D>();

            // Verifica a dire��o do player usando o isFacingRight
            float direction = isFacingRight ? 1 : -1;

            // Atribui uma velocidade ao proj�til considerando a velocidade do jogador
            float bulletSpeed = speedBulllets + Mathf.Abs(rg.velocity.x); // Soma a velocidade do proj�til � do jogador
            firerb.velocity = new Vector2(direction * bulletSpeed, 0);

            Destroy(newFire, 6f);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chao")
        {
            CnJmp = true;
            //tempinho = 0.5f;
        }

    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "chao")
    //    {
    //        tempinho -= Time.deltaTime;

    //    }
    //}
}
