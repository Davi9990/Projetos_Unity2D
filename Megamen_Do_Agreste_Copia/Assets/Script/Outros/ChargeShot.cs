using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot : MonoBehaviour
{
    public float speed; // Velocidade do tiro
    public Transform Hand; // Referência à mão do player
    private int danoAtual = 1; // Dano do tiro
    private float lado = 1; // Lado do disparo (1 para direita, -1 para esquerda)
    private bool carregou = false; // Controle se o tiro foi carregado
    private float tempoCarregando = 0f; // Tempo carregando o tiro
    private float tempoMaxCarregamento = 1.5f; // Tempo máximo de carregamento
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Garantir que o tiro seja instanciado na posição da mão
        if (Hand != null)
        {
            transform.position = Hand.position;
        }
    }

    void Update()
    {
        // Determina a direção do disparo com base na orientação do player
        lado = Move.virou ? -1 : 1;

        // Atualiza a posição do projétil enquanto o tiro não foi disparado
        if (!carregou && Hand != null)
        {
            transform.position = Hand.position;
        }

        // Controle do carregamento do tiro
        CarregandoTiro();
    }

    void CarregandoTiro()
    {
        // Segurando a tecla para carregar o tiro
        if (Input.GetKey(KeyCode.J) && !carregou)
        {
            // Incrementa o tempo de carregamento
            tempoCarregando = Mathf.Clamp(tempoCarregando + Time.deltaTime, 0, tempoMaxCarregamento);
            danoAtual = (int)Mathf.Round(tempoCarregando); // Define o dano com base no tempo carregado
        }
        // Soltando a tecla para disparar
        else if (Input.GetKeyUp(KeyCode.J) && !carregou)
        {
            carregou = true; // Indica que o tiro foi disparado
            rb.velocity = new Vector2(lado * speed, 0); // Define a direção do disparo

            Debug.Log($"Dano do tiro: {danoAtual}");
            Destroy(this.gameObject, 6f); // Destrói o projétil após 6 segundos
        }
    }
}
