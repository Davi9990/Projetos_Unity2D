using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRb : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
       
        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");   

        
        Vector3 move = new Vector3(x, 0, z) * speed;

        
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision other)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
}
