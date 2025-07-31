using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativade_Torque : MonoBehaviour
{
    private ConstantForce2D cf;

    public float torqueValue = 100f;


    void Start()
    {
        cf = GetComponent<ConstantForce2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            cf.torque = torqueValue;
        }
        else
        {
            cf.torque = 0;
        }
    }
}
