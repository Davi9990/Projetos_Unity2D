using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selecao_de_Armas : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] weaponSprites;

    // �ndice atual do sprite selecionado
    private int currentWeaponIndex = 0;

    // Vari�veis para armazenar a posi��o inicial e final do toque
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
                // Armazenar a posi��o inicial do toque
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Armazenar a posi��o final do toque
                endTouchPosition = touch.position;

                // Calcular a diferen�a entre as posi��es inicial e final
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                // Verificar se o movimento foi um swipe horizontal
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                    {
                        // Swipe para a direita - pr�ximo sprite
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
        // Alterar o �ndice do sprite com base na dire��o do swipe
        currentWeaponIndex += direction;

        // Garantir que o �ndice permane�a dentro dos limites do array
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
