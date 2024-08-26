using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_GetAxisMouse1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float T = Input.GetAxis("MouseX");
        float V = Input.GetAxis("MouseY");

        transform.Translate(new Vector3 (T * Time.deltaTime, V * Time.deltaTime,0));

        //float w = Input.GetAxis("Mouse ScrollWheel");

        //transform.Translate(new Vector3(w * Time.deltaTime, 0, 0));
    }
}
