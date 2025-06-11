using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public float swingSpeed = 2f;
    public float maxswingAngle = 30f;

    private float startAngle;
    private float direction = 1f;


    void Start()
    {
        startAngle = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = startAngle + Mathf.Sin(Time.time * swingSpeed) * maxswingAngle;
        transform.rotation = Quaternion.Euler(0, 0, angle * direction);
    }
}
