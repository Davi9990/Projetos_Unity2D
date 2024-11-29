using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarBuxaPodeApagarEssaPorraDavi : MonoBehaviour
{
    public float vel = 2; // Velocidade padrão
    private int direcao = 1; // Direção padrão (1 para direita, -1 para esquerda)

    void Start()
    {
        StartCoroutine(Sumir());
    }

    void Update()
    {
        // Movimenta o projétil na direção correta
        transform.Translate(new Vector2(direcao * vel * Time.deltaTime, 0));
    }

    // Define a direção do projétil ao ser criado
    public void ConfigurarDirecao(int novaDirecao)
    {
        direcao = novaDirecao; // -1 para esquerda, 1 para direita
    }

    IEnumerator Sumir()
    {
        Debug.Log($"Atirou {this.gameObject.name}!");
        yield return new WaitForSecondsRealtime(3f);

        Destroy(this.gameObject);
    }
}
