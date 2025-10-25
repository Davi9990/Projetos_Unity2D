using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustarTelaDoCelular : MonoBehaviour
{    
    void Awake()
    {
        // 🔒 Trava o jogo na horizontal
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // 🔧 Usa a resolução nativa do dispositivo
        int largura = Screen.currentResolution.width;
        int altura = Screen.currentResolution.height;

        // Ajusta a resolução com fullscreen
        Screen.SetResolution(largura, altura, true);

        // Mantém a qualidade do DPI (escala automática de pixels)
        QualitySettings.resolutionScalingFixedDPIFactor = 1f;

        // Evita travamentos de FPS por variações de resolução
        Application.targetFrameRate = 60;
    }
}
