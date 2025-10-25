using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanaPickup : MonoBehaviour
{
    [HideInInspector]
    public GeradorCanas gerador;

    public AudioClip somColeta;
    AudioSource audioSource;
    bool coletada = false;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Pegar(CarrocaCarga carroca)
    {
        if (coletada) return;
        coletada = true;

        if (somColeta && audioSource) audioSource.PlayOneShot(somColeta);

        var sr = GetComponent<SpriteRenderer>();
        if (sr) sr.enabled = false;
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        if (carroca != null) carroca.AdicionarCana(gameObject);

        if (gerador) gerador.RegistrarColeta(this);
    }
}