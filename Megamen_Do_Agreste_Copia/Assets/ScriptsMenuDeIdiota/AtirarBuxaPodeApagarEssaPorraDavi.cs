using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarBuxaPodeApagarEssaPorraDavi : MonoBehaviour
{
    public float vel = 2; // Velocidade padr�o
    private int direcao = 1; // Dire��o padr�o (1 para direita, -1 para esquerda)

    void Start()
    {
        StartCoroutine(Sumir());
    }

    void Update()
    {
        // Movimenta o proj�til na dire��o correta
        transform.Translate(new Vector2(direcao * vel * Time.deltaTime, 0));
    }

    // Define a dire��o do proj�til ao ser criado
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
