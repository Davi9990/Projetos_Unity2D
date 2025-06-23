using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpByEnergy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float massa = 1f;
    public float alturaInicial = 5f;
    public float energiaPerdida = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Saltar();
    }

    public void Saltar()
    {
        float energiaPotencial = massa * 9.81f *  alturaInicial;
        float energiaUtilizavel = energiaPotencial * (1f - energiaPerdida);
        float velocidadeFinal = Mathf.Sqrt((2 * energiaUtilizavel) / massa);

        rb.velocity = new Vector2(rb.velocity.x, velocidadeFinal);
    }
}
