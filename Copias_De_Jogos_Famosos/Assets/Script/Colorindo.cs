using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorindo : MonoBehaviour
{
    public Color[] correctSequence; // Sequência correta de cores para vencer
    private Color[] playerSequence; // Sequência que o jogador está montando

    public SpriteRenderer[] squares; // SpriteRenderers dos quadrados a serem pintados
    public SpriteRenderer[] paletteColors; // SpriteRenderers das cores na paleta
    public SpriteRenderer victoryText; // Letreiro de vitória
    public GameObject door; // Porta a ser ocultada quando o jogador vencer

    private Color selectedColor; // Cor selecionada pelo jogador na paleta
    private int currentStep = 0; // Posição atual na sequência do jogador

    private void Start()
    {
        victoryText.enabled = false; // Esconde o letreiro de vitória inicialmente
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

        // Verifica se a cor está correta na sequência
        if (selectedColor == correctSequence[currentStep])
        {
            playerSequence[currentStep] = selectedColor; // Atualiza a sequência do jogador
            currentStep++;

            // Verifica se o jogador completou a sequência
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
        victoryText.enabled = true; // Mostra o letreiro de vitória
        door.SetActive(false); // Oculta a porta
    }

    private void ResetGame()
    {
        currentStep = 0;
        playerSequence = new Color[correctSequence.Length]; // Reseta a sequência do jogador

        // Limpa os quadrados, definindo a cor para branco ou outra cor padrão
        foreach (var square in squares)
        {
            square.color = Color.white; // Define a cor padrão (ex. branco)
        }
    }
}
