using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public int dano = 1;
    public float velocidade = 5f;
    public float attackCooldown = 2f;
    public bool Chupando;
    public float TempoChupando;
    private float lastAttackTime;

    private Rigidbody2D rb;
    private SistemaDeVida vida;
    public Transform player;
    public Rigidbody2D PlayerRb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidade * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if(vida != null)
            {
                Chupando = true;
                vida.vida -= dano;

                StartCoroutine(ChupandoJogador());
            }
        }
    }

    private IEnumerator ChupandoJogador()
    {
        Debug.Log("Chupando (a vida) do Jogador");
       
        dano++;

        PlayerRb.velocity = Vector2.zero;
        PlayerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        lastAttackTime = Time.time;

       
        Chupando = false;
        LibertarJogador();
        yield return new WaitForSeconds(TempoChupando);
    }

    private void LibertarJogador()
    {
        PlayerRb.constraints = RigidbodyConstraints2D.None;
        PlayerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
