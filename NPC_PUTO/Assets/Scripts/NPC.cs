using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Fugindo do Jogador")]
    public GameObject Player;
    public float DistanceRun = 5f;
    public float speed;

    public float TimeHunger;
    public bool TimeToEat;
    public float DistanceFood;
    public GameObject Food;
    //public GameObject House;

    public bool fugindo;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (TimeHunger > 0)
        {
            TimeHunger -= Time.deltaTime;
        }

        if(TimeHunger <= 100)
        {
            TimeToEat = true;
        }

        DetectendoComida();
        IndoSeSaciar();
        DetectandoJogador();
        Correndo();
    }

    public void Correndo()
    {
        if (fugindo)
        {
            Vector2 direction = (Player.transform.position - transform.position).normalized;
            rb.velocity = -direction * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void DetectandoJogador()
    {
        Vector2 player_position = Player.transform.position;
        Vector2 myPosition = transform.position;

        float distance = Vector2.Distance(myPosition, player_position);

        fugindo = distance < DistanceRun;

    }

    public void DetectendoComida()
    {
        Vector2 food_position = Food.transform.position;
        Vector2 myposition2 = transform.position;

        float distanceFood = Vector2.Distance(myposition2, food_position);

        TimeToEat = distanceFood < DistanceFood;
    }

    public void IndoSeSaciar()
    {

        if (TimeToEat && !fugindo)
        {
            Vector2 Indoeat = Vector2.MoveTowards(rb.position, Food.transform.position, speed * Time.deltaTime);
            rb.MovePosition(Indoeat);
        }
        else if(fugindo)
        {
            TimeToEat = false;
            Correndo();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DistanceRun);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DistanceFood);
    }
}
