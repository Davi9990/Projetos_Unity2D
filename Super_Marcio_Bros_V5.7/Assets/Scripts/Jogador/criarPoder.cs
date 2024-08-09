using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class criarPoder : MonoBehaviour
{
    public GameObject hand;
    public GameObject fire; // Prefab do proj�til
    public bool liberaPoder = false; // Controle para libera��o do poder
    public Rigidbody2D rb;
    public float speed = 5f;
    public Vector2 movement;

    public float fireRate = 1f; // Tempo de espera entre disparos
    public float nextFireTime = 0f; // Tempo do pr�ximo disparo permitido

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Atribui��o do Rigidbody2D ao personagem
    }

    void Update()
    {
        if (liberaPoder && Time.time >= nextFireTime) // Controle para libera��o do poder
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                FireProjectile();
            }
        }

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Controle de movimento
    }

    private void FireProjectile()
    {
        GameObject newfire = Instantiate(fire, hand.transform.position, Quaternion.identity);
        Destroy(newfire, 5f); // Destroi o proj�til ap�s 5 segundos
        nextFireTime = Time.time + fireRate; // Define o pr�ximo tempo de disparo permitido
    }

    private void OnTriggerEnter2D(Collider2D Colisor) // Intera��o com o item (flor)
    {
        if (Colisor.gameObject.CompareTag("Flor"))
        {
            liberaPoder = true;

            // Refer�ncia ao PlayerHealth para aumentar a vida
            Barra_de_Vida playerHealth = GetComponent<Barra_de_Vida>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseHealth(1); // Aumenta a vida do personagem em 1
            }

            Destroy(Colisor.gameObject); // Destr�i o item coletado
        }
    }

    private void FixedUpdate() // Controle de movimento
    {
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction) // Controle de movimento
    {
        rb.AddForce(direction * speed);
    }
}
