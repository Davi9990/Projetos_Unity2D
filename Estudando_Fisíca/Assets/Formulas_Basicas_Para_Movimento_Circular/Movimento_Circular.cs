using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento_Circular : MonoBehaviour
{
    public Transform centro;
    public float raio = 2f;
    public float velocidade = 1f;

    private float angulo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angulo += velocidade * Time.deltaTime * 2 * Mathf.PI;

        float x = centro.position.x + raio * Mathf.Cos(angulo);
        float y = centro.position.y + raio * Mathf.Sin(angulo);

        transform.position = new Vector3(x, y, 0);
    }
}
