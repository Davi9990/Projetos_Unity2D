using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vetores : MonoBehaviour
{
    //Adicione uma referencia ao jogador, que é definida na janela do Inspector
    public GameObject player;

    //Defina a distancia na qual o inimigo ataca
    public float distancia_de_Ataque = 15f;

    public float velocidade = 3f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pegue a posição do jogador e a posição deste GameObject
        Vector2 playerPosition = player.transform.position;
        Vector2 myPosition = transform.position;

        //Use Vector2.Distance para obter a distancia entre o Player e o inimigo
        float distance = Vector2.Distance(myPosition, playerPosition);

        //Verifique se o jogador está perto o suficiente
        if(distance < distancia_de_Ataque)
        {
            Attack(player);
        }
    }

    void Attack(GameObject target)
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * velocidade;
    }
}
