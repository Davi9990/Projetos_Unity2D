using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirando_Com_GetTouch : MonoBehaviour
{
    public GameObject projectile;
    public GameObject clone;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < Input.touchCount;i++)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Began)
            {
                clone = Instantiate(projectile, transform.position,transform.rotation) as GameObject;
            }
        }
    }
}
