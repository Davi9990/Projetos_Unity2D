using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heroi_De_Tres_Poderes : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private int direcao = 1;
    private bool isFacingRight;

    public Transform Hand;
    public GameObject Bullets;
    public float speedBullets;
    public float fireRate = 1f;
    public float nextFireTime = 0f;

    private float tempoUltimoToque = 0f;
    private float intervaloToque = 0.3f;
    private SpriteRenderer sprite;

    private float intervaloToque2 = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Mover();

                if(touch.position.x < Screen.width/2)
                    direcao = -1;
                else
                    direcao = 1;

                if(Time.time - tempoUltimoToque < intervaloToque)
                {
                    Atirar();
                }

                else if(Time.time - tempoUltimoToque < intervaloToque2)
                {
                    sprite.color = Color.red;
                }
            }
        }
    }

    void Mover()
    {
        transform.Translate(new Vector2(direcao * speed * Time.deltaTime,0));
    }

    void Atirar()
    {
        GameObject newFire = Instantiate(Bullets, Hand.position, Quaternion.identity);
        // Acessa o Rigidbody2D do projétil instanciado
        Rigidbody2D firerb = newFire.GetComponent<Rigidbody2D>();

        // Verifica a direção do player usando o isFacingRight
        float direction = isFacingRight ? 1 : -1;

        // Atribui uma velocidade ao projétil considerando a velocidade do jogador
        float bulletSpeed = speedBullets + Mathf.Abs(rb.velocity.x); // Soma a velocidade do projétil à do jogador
        firerb.velocity = new Vector2(direction * bulletSpeed, 0);

        Destroy(newFire, 6f);
        nextFireTime = Time.time + fireRate;
    }
}
