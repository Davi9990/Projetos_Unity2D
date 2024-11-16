using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Moobile : MonoBehaviour
{
    // Movimenta��o
    public float speed = 5;
    private int direcao = 0;
    public bool isMoving = false; // Define se o jogador est� se movendo
    private bool isFacingRight = true;

    // Pulos
    public float jumpForce = 6;
    private bool estaNoChao = false;
    private Rigidbody2D rb;

    //Atirar
    public Transform Hand;
    public GameObject Bullets;
    public float speedBullets;
    public float fireRate = 1f;
    public float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMoving) // S� move se isMoving for verdadeiro
        {
            Mover();
        }
    }

    // Movimenta o jogador na dire��o atual
    void Mover()
    {
        transform.Translate(new Vector2(direcao * speed * Time.deltaTime, 0));

        if(direcao > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        else if(direcao < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    // Fun��o para pular
    public void Jump()
    {
        if (estaNoChao)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            estaNoChao = false;
        }
    }

    // Detecta quando o jogador toca o ch�o
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }

    // Define dire��o para a direita
    public void Direita()
    {
        direcao = 1;
        isMoving = true; // Come�a a se mover
    }

    // Define dire��o para a esquerda
    public void Esquerda()
    {
        direcao = -1;
        isMoving = true; // Come�a a se mover
    }

    // Para a movimenta��o
    public void Parar()
    {
        direcao = 0;
        isMoving = false; // Para de se mover
    }

    public void Atirar()
    {
        GameObject newFire = Instantiate(Bullets, Hand.position, Quaternion.identity);
        // Acessa o Rigidbody2D do proj�til instanciado
        Rigidbody2D firerb = newFire.GetComponent<Rigidbody2D>();

        // Verifica a dire��o do player usando o isFacingRight
        float direction = isFacingRight ? 1 : -1;

        // Atribui uma velocidade ao proj�til considerando a velocidade do jogador
        float bulletSpeed = speedBullets + Mathf.Abs(rb.velocity.x); // Soma a velocidade do proj�til � do jogador
        firerb.velocity = new Vector2(direction * bulletSpeed, 0);

        Destroy(newFire, 6f);
        nextFireTime = Time.time + fireRate;
    }
}
