using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selecao_de_Armas : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] weaponSprites;

    // Índice atual do sprite selecionado
    private int currentWeaponIndex = 0;

    // Variáveis para armazenar a posição inicial e final do toque
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    void Update()
    {
        // Detectar toque na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Armazenar a posição inicial do toque
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Armazenar a posição final do toque
                endTouchPosition = touch.position;

                // Calcular a diferença entre as posições inicial e final
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                // Verificar se o movimento foi um swipe horizontal
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                    {
                        // Swipe para a direita - próximo sprite
                        ChangeSprite(1);
                    }
                    else
                    {
                        // Swipe para a esquerda - sprite anterior
                        ChangeSprite(-1);
                    }
                }
            }
        }
    }

    public void ChangeSprite(int direction)
    {
        // Alterar o índice do sprite com base na direção do swipe
        currentWeaponIndex += direction;

        // Garantir que o índice permaneça dentro dos limites do array
        if (currentWeaponIndex >= weaponSprites.Length)
        {
            currentWeaponIndex = 0;
        }
        else if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weaponSprites.Length - 1;
        }

        // Alterar o sprite do SpriteRenderer
        if (spriteRenderer != null && weaponSprites != null && weaponSprites.Length > 0)
        {
            spriteRenderer.sprite = weaponSprites[currentWeaponIndex];
        }
    }
}
