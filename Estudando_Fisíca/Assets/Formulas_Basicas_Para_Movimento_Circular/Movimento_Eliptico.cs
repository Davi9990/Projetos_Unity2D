using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento_Eliptico : MonoBehaviour
{
    public Transform centerpoint;
    public float semiMajorAxis = 5f;
    public float semiMinorAxis = 3f;
    public float orbitSpeed = 1f;

    private float angle;

    // Update is called once per frame
    void Update()
    {
        angle += orbitSpeed * Time.deltaTime;
        float x = semiMajorAxis * Mathf.Cos(angle);
        float y = semiMinorAxis * Mathf.Sin(angle);

        transform.position = new Vector2(

            centerpoint.position.x + x,
            centerpoint.position.y + y
            );
    }
}
