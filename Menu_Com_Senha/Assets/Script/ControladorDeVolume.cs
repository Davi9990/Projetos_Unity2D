using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeVolume : MonoBehaviour
{
    public AudioSource audioSource; // Componente de �udio que ser� controlado
    public Scrollbar volumeScrollbar; // Scrollbar para controle do volume

    void Start()
    {
        // Configura o valor inicial do Scrollbar com base no volume do �udio atual
        volumeScrollbar.value = audioSource.volume;

        // Adiciona um listener para o controle de volume
        volumeScrollbar.onValueChanged.AddListener(SetVolume);
    }

    // M�todo para ajustar o volume do �udio
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
