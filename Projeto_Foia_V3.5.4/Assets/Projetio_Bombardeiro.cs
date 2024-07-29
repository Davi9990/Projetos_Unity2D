using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetio_Bombardeiro : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 gravity = new Vector3(0,-9.81f,0);

    void Start()
    {
        rb = GetComponent<Rigidbody>();     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity += gravity * Time.fixedDeltaTime;
    }
}
