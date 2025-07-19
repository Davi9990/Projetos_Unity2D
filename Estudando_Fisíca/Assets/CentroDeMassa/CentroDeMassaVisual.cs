using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Centro_De_Massa_Visual : MonoBehaviour
{
    public Vector2 centroDeMassaLocal = new Vector2 (0, -1f);
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = centroDeMassaLocal;

        // Criar marcador visual
        GameObject marcador = new GameObject("CentroMassa");
        marcador.transform.SetParent(transform);
        marcador.transform.localPosition = centroDeMassaLocal;

        var sr = marcador.AddComponent<SpriteRenderer>();
        sr.color = Color.red;
        marcador.transform.localScale = Vector3.one * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
