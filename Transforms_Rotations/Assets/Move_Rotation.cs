using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Rotation : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float rotationSpeed = 720f;
    private Vector3 moveDirection;
    private Transform playerTransform;

    // Input para o movimento
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        // Armazena o transform do jogador para otimizar o uso
        playerTransform = transform;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Calcula a dire��o de movimento baseado na entrada
        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Move o personagem
        MoveTranslatePlayer();

        // Rotaciona o personagem em dire��o ao movimento
        if (moveDirection.magnitude > 0.1f)
        {
            RotatePlayer();
        }
    }

    void MoveTranslatePlayer()
    {
        // Movimenta o player no espa�o global
        playerTransform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void RotatePlayer()
    {
        // Calcula o �ngulo de rota��o baseado na dire��o do movimento
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

        // Interpola suavemente a rota��o do player para a dire��o alvo
        float smoothedAngle = Mathf.LerpAngle(playerTransform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime / 360f);

        // Aplica a rota��o
        playerTransform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
    }
}
