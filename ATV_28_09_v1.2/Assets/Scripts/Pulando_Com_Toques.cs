using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulando_Com_Toques : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    private int jumpCount = 0;
    private float lastTapTime = 0f;

    // Tempo máximo entre dois toques rápidos para considerar como um "toque duplo"
    public float doubleTapTimeWindow = 0.3f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Verificar se o toque começou
            if (touch.phase == TouchPhase.Began)
            {
                float currentTapTime = Time.time;

                // Verificar se é um toque duplo (dois toques rápidos consecutivos)
                if (currentTapTime - lastTapTime <= doubleTapTimeWindow)
                {
                    if (jumpCount < 2)
                    {
                        Jump();
                        jumpCount++;
                    }
                }
                else
                {
                    // Reiniciar o contador de saltos ao receber um toque que não seja rápido o suficiente
                    jumpCount = 0;
                }

                lastTapTime = currentTapTime;
            }
        }
    }

    void Jump()
    {
        // Remover a velocidade vertical antes de aplicar a força do salto para garantir a consistência
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reiniciar o contador de saltos ao tocar no chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
