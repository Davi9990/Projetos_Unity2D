using UnityEngine;

public class areaIlumicacao : MonoBehaviour
{
    // Vari�veis p�blicas para controle no Inspector
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.0f;
    public float flickerSpeed = 0.2f;

    // Refer�ncia para o componente Light
    private Light myLight;
    private float randomValue;

    void Start()
    {
        myLight = GetComponent<Light>();
        randomValue = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(randomValue, Time.time * flickerSpeed);
        myLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
