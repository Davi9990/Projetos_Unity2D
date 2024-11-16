using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bola : MonoBehaviour
{
   Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeEnd, timeInterval;

    public TextMeshProUGUI Text; // Texto para mostrar a pontuação
    public int Pontuacao = 0;

    [Range(0.05f, 1f)]
    public float throwForce = 0.3f;

    public Transform cesta; // Posição da cesta para calcular a distância
    private bool entrouPorBaixo = false; // Verifica se a bola entrou por baixo

    // Distâncias para definir os pontos
    public float distanciaParaTresPontos = 14.69f; // Distância mínima para 3 pontos
    public float distanciaParaDoisPontos = 10.9f; // Distância máxima para 2 pontos

    void Start()
    {
        AtualizarTexto();
    }

    void Update()
    {
        // Monitora a distância da bola para a cesta constantemente no console
        MostrarDistanciaBolaCesta();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchTimeEnd = Time.time;
            timeInterval = touchTimeEnd - touchTimeStart;
            endPos = Input.GetTouch(0).position;
            direction = endPos - startPos;
            GetComponent<Rigidbody2D>().AddForce(direction / timeInterval * throwForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CestaBase"))
        {
            // Marca que a bola entrou por baixo, impedindo a pontuação
            entrouPorBaixo = true;
        }

        if (collision.gameObject.CompareTag("CestaTopo"))
        {
            // Verifica se a bola NÃO entrou por baixo
            if (!entrouPorBaixo)
            {
                // Calcula a distância da bola até a cesta para definir a pontuação
                float distancia = Vector2.Distance(transform.position, cesta.position);
                Debug.Log("Distância entre a bola e a cesta: " + distancia.ToString("F2")); // Mostrar distância no console

                // Verifica a distância para 3 pontos ou 2 pontos
                if (distancia <= distanciaParaTresPontos) // Se a distância for menor ou igual ao limite para 3 pontos
                {
                    Pontuacao += 3; // Marca 3 pontos
                }
                else if (distancia <= distanciaParaDoisPontos) // Se a distância for menor ou igual ao limite para 2 pontos
                {
                    Pontuacao += 2; // Marca 2 pontos
                }

                AtualizarTexto();
            }

            // Reinicia a verificação para a próxima jogada
            entrouPorBaixo = false;
        }
    }

    void AtualizarTexto()
    {
        Text.text = Pontuacao.ToString();
    }

    // Função para mostrar a distância entre a bola e a cesta no Console
    void MostrarDistanciaBolaCesta()
    {
        float distancia = Vector2.Distance(transform.position, cesta.position);
        Debug.Log("Distância entre a bola e a cesta: " + distancia.ToString("F2"));
    }
}
