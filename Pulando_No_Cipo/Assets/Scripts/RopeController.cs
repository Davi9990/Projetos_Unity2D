using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public float swingSpeed = 2f;
    public float maxSwingAngle = 30f;

    private float startAngle;
    private float direction = 1f;

    void Start()
    {
        startAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        float angle = startAngle + Mathf.Sin(Time.time * swingSpeed) * maxSwingAngle;
        transform.rotation = Quaternion.Euler(0, 0, angle * direction);
    }
}
