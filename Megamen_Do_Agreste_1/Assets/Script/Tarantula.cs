using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarantula : MonoBehaviour
{
    public Transform player; //referencia ao transform do Jogador
    public float Distancia = 10f; //Distancia em que o inimigo começa a seguir o jogador
    public float velocidade = 2f;
    private SpriteRenderer Render; //Referencia do sprite para o flip
    public bool Descendo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
