using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeVolume : MonoBehaviour
{
    public AudioSource audioSource; // Componente de áudio que será controlado
    public Scrollbar volumeScrollbar; // Scrollbar para controle do volume

    void Start()
    {
        // Configura o valor inicial do Scrollbar com base no volume do áudio atual
        volumeScrollbar.value = audioSource.volume;

        // Adiciona um listener para o controle de volume
        volumeScrollbar.onValueChanged.AddListener(SetVolume);
    }

    // Método para ajustar o volume do áudio
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
