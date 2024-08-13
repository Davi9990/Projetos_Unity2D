using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class criarPoder : MonoBehaviour
{
    public GameObject hand; // Objeto que representará a posição de onde o projétil será disparado
    public GameObject fire; // Prefab do projétil que será instanciado ao disparar
    public bool liberaPoder = false; // Variável de controle que determina se o poder está liberado
    public Rigidbody2D rb; // Referência ao Rigidbody2D do personagem, usado para o movimento
    public float speed = 5f; // Velocidade de movimento do personagem
    public Vector2 movement; // Vetor que armazena o movimento horizontal e vertical do personagem

    public float fireRate = 1f; // Tempo de espera entre disparos, em segundos
    public float nextFireTime = 0f; // Armazena o tempo em que o próximo disparo será permitido

    void Start()
    {
        // Inicializa a referência ao Rigidbody2D do personagem no início do jogo
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica se o poder está liberado e se o tempo atual permite um novo disparo
        if (liberaPoder && Time.time >= nextFireTime)
        {
            // Verifica se a tecla 'J' foi pressionada para disparar o projétil
            if (Input.GetKeyDown(KeyCode.J))
            {
                FireProjectile(); // Chama o método para disparar o projétil
            }
        }

        // Captura a entrada do jogador para movimento horizontal e vertical
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FireProjectile()
    {
        // Instancia o projétil na posição da "mão" do personagem
        GameObject newfire = Instantiate(fire, hand.transform.position, Quaternion.identity);

        // Destroi o projétil após 5 segundos para evitar acumulação desnecessária de objetos na cena
        Destroy(newfire, 5f);

        // Atualiza o tempo do próximo disparo permitido, baseado na taxa de disparo (fireRate)
        nextFireTime = Time.time + fireRate;
    }

    private void OnTriggerEnter2D(Collider2D Colisor)
    {
        // Verifica se o personagem colidiu com um objeto que possui a tag "Flor"
        if (Colisor.gameObject.CompareTag("Flor"))
        {
            // Libera o poder para permitir que o jogador dispare projéteis
            liberaPoder = true;

            // Obtém a referência ao script Barra_de_Vida para aumentar a vida do personagem
            Barra_de_Vida playerHealth = GetComponent<Barra_de_Vida>();
            if (playerHealth != null)
            {
                // Aumenta a vida do personagem em 1 ao coletar a "Flor"
                playerHealth.IncreaseHealth(1);
            }

            // Destroi o objeto "Flor" após ser coletado
            Destroy(Colisor.gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Chama o método para movimentar o personagem de acordo com a entrada do jogador
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        // Aplica uma força no Rigidbody2D do personagem para movimentá-lo na direção desejada
        rb.AddForce(direction * speed);
    }
}
