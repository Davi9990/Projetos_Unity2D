using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Movimentação
    public float speed;
    private Rigidbody2D rg;
    private bool isFacingRight = true;

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

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
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

        //Verifica a direção do movimento e inverte o sprite se necessario

        if (n > 0 && !isFacingRight)
        {
            //Inverte a direção do sprite
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        else if (n < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    void pular()
    {
        if (Input.GetButtonDown("Jump") && CnJmp == true)
        {
            rg.AddForce(new Vector2(0, JP), ForceMode2D.Impulse);
            CnJmp = false;
        }

        // Pulo variável: reduz a velocidade do pulo se o jogador soltar o botão antes do pulo atingir o ponto máximo
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
            // Acessa o Rigidbody2D do projétil instanciado
            Rigidbody2D firerb = newFire.GetComponent<Rigidbody2D>();

            // Verifica a direção do player usando o isFacingRight
            float direction = isFacingRight ? 1 : -1;

            // Atribui uma velocidade ao projétil considerando a velocidade do jogador
            float bulletSpeed = speedBulllets + Mathf.Abs(rg.velocity.x); // Soma a velocidade do projétil à do jogador
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
