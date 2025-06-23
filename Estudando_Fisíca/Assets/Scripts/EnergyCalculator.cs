using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyCalculator : MonoBehaviour
{
    public Rigidbody2D rb;
    public float massa = 1f;
    public float alturaReferencia = 0f;
    public TMP_Text textoEnergia;

    private const float gravidade = 9.81f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float velocidade = rb.velocity.magnitude;//Modulo da velocidade
        float altura = rb.position.y - alturaReferencia;

        float energiaCinetica = 0.5f * massa * Mathf.Pow(velocidade, 2);
        float energiaPotencial = massa * gravidade * altura;
        float energiaMecanica = energiaCinetica + energiaPotencial;

        textoEnergia.text = $"EC: {energiaCinetica:F2} J\nEP: {energiaPotencial:F2} J\nEM: {energiaMecanica:F2} J";
    }
}
