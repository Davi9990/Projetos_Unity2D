using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodaGirando : MonoBehaviour
{
    public float velocidade = 300f; // Velocidade da rotação

    void Update()
    {
        // Rodar a roda do moinho sem precisar de animacao so pelo codigo
        transform.Rotate(Vector3.forward  * velocidade * Time.deltaTime);
    }
}
