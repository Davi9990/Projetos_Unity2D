using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyRoomControl : MonoBehaviour
{
    public GameObject cup1, cup2, cup3;

    int cup16Got, cup26Got, cup36Got;

    void Start()
    {
        cup16Got = PlayerPrefs.GetInt("cup15Got");
        cup26Got = PlayerPrefs.GetInt("cup26Got");
        cup36Got = PlayerPrefs.GetInt("cuo36Got");

        if(cup16Got == 1)
        {
            cup1.SetActive(true);
        }
        else
        {
            cup1.SetActive(false);
        }

        if (cup26Got == 1)
        {
            cup2.SetActive(true);
        }
        else
        {
            cup2.SetActive(false);
        }

        if (cup36Got == 1)
        {
            cup3.SetActive(true);
        }
        else
        {
            cup3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
