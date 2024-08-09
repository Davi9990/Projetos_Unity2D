using UnityEngine;
using System.Collections;

public class BossHpManager : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;
    private SpriteRenderer renderizadorDeSprite;

    void Start()
    {
        vidaAtual = vidaMaxima;
        renderizadorDeSprite = GetComponent<SpriteRenderer>();
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        StartCoroutine(FeedbackDeDano());

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private IEnumerator FeedbackDeDano()
    {
        // Altera a cor do Boss para branco para indicar que ele levou dano
        renderizadorDeSprite.color = Color.red;
        yield return new WaitForSeconds(2.0f);
        renderizadorDeSprite.color = Color.white;  // Ajuste para a cor original do Boss, se não for branco
    }

    private void Morrer()
    {
        // Código para lidar com a morte do Boss (pode ser expandido conforme necessário)
        Debug.Log("Boss morreu!");
        Destroy(gameObject);
    }
}
