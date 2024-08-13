using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class criarPoder : MonoBehaviour
{
    public GameObject hand; // Objeto que representar� a posi��o de onde o proj�til ser� disparado
    public GameObject fire; // Prefab do proj�til que ser� instanciado ao disparar
    public bool liberaPoder = false; // Vari�vel de controle que determina se o poder est� liberado
    public Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do personagem, usado para o movimento
    public float speed = 5f; // Velocidade de movimento do personagem
    public Vector2 movement; // Vetor que armazena o movimento horizontal e vertical do personagem

    public float fireRate = 1f; // Tempo de espera entre disparos, em segundos
    public float nextFireTime = 0f; // Armazena o tempo em que o pr�ximo disparo ser� permitido

    void Start()
    {
        // Inicializa a refer�ncia ao Rigidbody2D do personagem no in�cio do jogo
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica se o poder est� liberado e se o tempo atual permite um novo disparo
        if (liberaPoder && Time.time >= nextFireTime)
        {
            // Verifica se a tecla 'J' foi pressionada para disparar o proj�til
            if (Input.GetKeyDown(KeyCode.J))
            {
                FireProjectile(); // Chama o m�todo para disparar o proj�til
            }
        }

        // Captura a entrada do jogador para movimento horizontal e vertical
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FireProjectile()
    {
        // Instancia o proj�til na posi��o da "m�o" do personagem
        GameObject newfire = Instantiate(fire, hand.transform.position, Quaternion.identity);

        // Destroi o proj�til ap�s 5 segundos para evitar acumula��o desnecess�ria de objetos na cena
        Destroy(newfire, 5f);

        // Atualiza o tempo do pr�ximo disparo permitido, baseado na taxa de disparo (fireRate)
        nextFireTime = Time.time + fireRate;
    }

    private void OnTriggerEnter2D(Collider2D Colisor)
    {
        // Verifica se o personagem colidiu com um objeto que possui a tag "Flor"
        if (Colisor.gameObject.CompareTag("Flor"))
        {
            // Libera o poder para permitir que o jogador dispare proj�teis
            liberaPoder = true;

            // Obt�m a refer�ncia ao script Barra_de_Vida para aumentar a vida do personagem
            Barra_de_Vida playerHealth = GetComponent<Barra_de_Vida>();
            if (playerHealth != null)
            {
                // Aumenta a vida do personagem em 1 ao coletar a "Flor"
                playerHealth.IncreaseHealth(1);
            }

            // Destroi o objeto "Flor" ap�s ser coletado
            Destroy(Colisor.gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Chama o m�todo para movimentar o personagem de acordo com a entrada do jogador
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        // Aplica uma for�a no Rigidbody2D do personagem para moviment�-lo na dire��o desejada
        rb.AddForce(direction * speed);
    }
}
