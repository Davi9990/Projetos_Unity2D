using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragmento_Grande : MonoBehaviour
{
    private SpriteRenderer sprite_Cristal;
    public Inventario inventario;
    public Dialogo dialogo;

    void Start()
    {
        sprite_Cristal = GetComponent<SpriteRenderer>();
        sprite_Cristal.color = Color.white;
    }

    private void OnMouseEnter()
    {
        sprite_Cristal.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        sprite_Cristal.color = Color.white;
    }

    public void OnMouseDown()
    {
        if (gameObject.CompareTag("Cristal"))
        {
            inventario.Index_Cristais += 1;
            dialogo.AbrirDialogoDireto(inventario.Index_Cristais);
            Destroy(gameObject);
            Debug.Log("Cristal destruido!");
        }
    }
}
