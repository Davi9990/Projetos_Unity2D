using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Musicacapoeira : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("Player"))
        {
            videoPlayer.Play();
            Debug.Log("Vídeo começa a tocar!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Stop();
            Debug.Log("Vídeo parou de tocar!");
        }
    }
}

