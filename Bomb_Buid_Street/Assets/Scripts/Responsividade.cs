using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Responsividade : MonoBehaviour
{
    [Header("Reference Resolution")]
    [SerializeField] private Vector2 referenceResolution = new Vector2(1920, 1080);

    [Header("Offset Adjustments")]
    [SerializeField] private Vector2 offsetPosition = Vector2.zero;
    [SerializeField] private Vector2 offsetScale = Vector2.one;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        AdjustUIElement();
    }

    void AdjustUIElement()
    {
        if (rectTransform == null) return;

        float scaleFactorX = Screen.width / referenceResolution.x;
        float scaleFactorY = Screen.height / referenceResolution.y;

        // Ajuste de escala
        rectTransform.localScale = new Vector3(scaleFactorX * offsetScale.x, scaleFactorY * offsetScale.y, 1f);

        // Ajuste de posição
        Vector2 newPosition = new Vector2(
            Screen.width * offsetPosition.x,
            Screen.height * offsetPosition.y
        );

        rectTransform.anchoredPosition = newPosition;
    }

    void Update()
    {
        // Atualiza o elemento caso a resolução mude
        AdjustUIElement();
    }
}
