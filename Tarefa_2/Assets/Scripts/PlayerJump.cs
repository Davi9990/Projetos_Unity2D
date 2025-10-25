using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb;
    public float jumpForce = 5f;

    void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        //Inscreve o método Jump() no callback performed
        //controls.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Jump()
    {
        //Simples impulso para cima
        if(Mathf.Abs(rb.velocity.x) < 3f) // Evita pulo direto
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
