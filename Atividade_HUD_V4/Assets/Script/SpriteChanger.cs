using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] weaponSprites;

    public void ChangeSprite(int weaponIndex)
    {
        if (spriteRenderer != null && weaponSprites != null && weaponSprites.Length > weaponIndex)
        {
            spriteRenderer.sprite = weaponSprites[weaponIndex];
        }
    }
}
