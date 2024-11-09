using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorindo : MonoBehaviour
{
    public Color[] correctSequence; // Sequ�ncia correta de cores para vencer
    private Color[] playerSequence; // Sequ�ncia que o jogador est� montando

    public SpriteRenderer[] squares; // SpriteRenderers dos quadrados a serem pintados
    public SpriteRenderer[] paletteColors; // SpriteRenderers das cores na paleta
    public SpriteRenderer victoryText; // Letreiro de vit�ria
    public GameObject door; // Porta a ser ocultada quando o jogador vencer

    private Color selectedColor; // Cor selecionada pelo jogador na paleta
    private int currentStep = 0; // Posi��o atual na sequ�ncia do jogador

    private void Start()
    {
        victoryText.enabled = false; // Esconde o letreiro de vit�ria inicialmente
        playerSequence = new Color[correctSequence.Length];
    }

    private void Update()
    {
        // Verifica clique do mouse (para teste no PC)
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Verifica se clicou em uma cor da paleta
                for (int i = 0; i < paletteColors.Length; i++)
                {
                    if (hit.collider.gameObject == paletteColors[i].gameObject)
                    {
                        SelectColor(paletteColors[i].color); // Seleciona a cor clicada
                        return;
                    }
                }

                // Verifica se clicou em um dos quadrados
                for (int i = 0; i < squares.Length; i++)
                {
                    if (hit.collider.gameObject == squares[i].gameObject)
                    {
                        PaintSquare(i); // Pinta o quadrado com a cor selecionada
                        return;
                    }
                }
            }
        }
    }

    private void SelectColor(Color color)
    {
        selectedColor = color; // Define a cor selecionada
    }

    private void PaintSquare(int squareIndex)
    {
        if (selectedColor == null || squareIndex >= squares.Length)
            return;

        // Define a cor do quadrado com a cor selecionada
        squares[squareIndex].color = selectedColor;

        // Verifica se a cor est� correta na sequ�ncia
        if (selectedColor == correctSequence[currentStep])
        {
            playerSequence[currentStep] = selectedColor; // Atualiza a sequ�ncia do jogador
            currentStep++;

            // Verifica se o jogador completou a sequ�ncia
            if (currentStep == correctSequence.Length)
            {
                Victory();
            }
        }
        else
        {
            // Reseta o progresso se a cor estiver errada
            ResetGame();
        }
    }

    private void Victory()
    {
        victoryText.enabled = true; // Mostra o letreiro de vit�ria
        door.SetActive(false); // Oculta a porta
    }

    private void ResetGame()
    {
        currentStep = 0;
        playerSequence = new Color[correctSequence.Length]; // Reseta a sequ�ncia do jogador

        // Limpa os quadrados, definindo a cor para branco ou outra cor padr�o
        foreach (var square in squares)
        {
            square.color = Color.white; // Define a cor padr�o (ex. branco)
        }
    }
}
