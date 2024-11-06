using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Carregado : MonoBehaviour
{
    public Transform Hand;
    public GameObject bala;
    public float tempoCarregando;
    public bool estaCarregando = false;
    public bool estaPronto = false;
    public float dano;
    public float velocidade;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TiroCarregado()
    {
       if(Input.GetKeyDown(KeyCode.C))
       {
            bala.transform.position = Hand.position;
       }
    }
}
