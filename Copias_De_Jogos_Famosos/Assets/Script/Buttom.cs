using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    public SpriteRenderer PortaFechada;
    public SpriteRenderer PortaAberta;
    public SpriteRenderer Vitoria;

    void Start()
    {
        PortaFechada.enabled = true;
        PortaAberta.enabled = false;
        Vitoria.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Clicando();
        //Vitoria.enabled = false;
    }

    void Clicando()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                PortaFechada.enabled = false;
                PortaAberta.enabled = true;
            }
        }
    }
}
