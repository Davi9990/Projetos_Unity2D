using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinlEffect : MonoBehaviour
{
    public Material material1;
    public Material material2;
    public Renderer objRenderer;
    void Start()
    {
        StartCoroutine(BlinkObject(0.5f));
    }
    IEnumerator BlinkObject(float interval)
    {
        while (true)
        {
            objRenderer.material = material1;
            yield return new WaitForSeconds(interval);
            objRenderer.material = material2;
            yield return new WaitForSeconds(interval);
        }
    }
}
