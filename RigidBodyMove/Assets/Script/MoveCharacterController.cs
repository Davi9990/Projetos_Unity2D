using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class MoveCharacterController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 2f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;
    public float mouseSencitivity = 100f;

    private CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        //Tranca o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        HandleJump();
        RotatePlayer();
    }

    void MovePlayer()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right *  moveX + transform.forward * moveZ;

        //Define a velocidade de movimento com base na entrada do jogador
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        //Move o jogador
        controller.Move(move * moveSpeed * Time.deltaTime);

        //Aplica gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void HandleJump()
    {
        //Verifica se o jogador pode pular
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Aplica a força do pulo
        }
    }

    void RotatePlayer()
    {
        //Captura a entrada do mouse para a rotação
        float mouseX = Input.GetAxis("Mouse X") * mouseSencitivity * Time.deltaTime;

        //Rotaciona o personegem na direção do movimento do mouse
        transform.Rotate(Vector3.up * mouseX);
    }
}
