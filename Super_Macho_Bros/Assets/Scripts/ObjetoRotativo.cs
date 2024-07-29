using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRotativo : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotaciona o GameObject no eixo Z (plano 2D)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
