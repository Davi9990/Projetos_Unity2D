using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEndTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Game_Controller gm = GameObject.FindObjectOfType<Game_Controller>();
            if (gm != null)
            {
                gm.RopeGrabbed();  
            }
        }
    }
}
