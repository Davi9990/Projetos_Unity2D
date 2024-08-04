using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnarPlat : MonoBehaviour
{
    public GameObject plat1, plat2;
    public float tempo;
    void Start()
    {
        tempo = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;
        if(tempo < 0)
        {
            Instantiate(plat1);
            Instantiate(plat2);
            tempo = 3f;
        }
    }
}
