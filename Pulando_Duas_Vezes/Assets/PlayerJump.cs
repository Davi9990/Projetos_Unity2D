using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Configuração do Pulo")]
    public float jumpForce = 7f;       // Força do pulo
    public int maxJumps = 2;           // Quantidade máxima de pulos (2 = duplo pulo)

    private int jumpCount;             // Contador de pulos

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
    }

    void Update()
    {
        DetectTouch();
    }

    void DetectTouch()
    {
        // Detecta toque na tela
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (jumpCount < maxJumps) // Se ainda pode pular
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reseta velocidade vertical
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Quando encostar no chão, reseta os pulos
        if (collision.gameObject.CompareTag("Chao"))
        {
            jumpCount = 0;
        }
    }
}
