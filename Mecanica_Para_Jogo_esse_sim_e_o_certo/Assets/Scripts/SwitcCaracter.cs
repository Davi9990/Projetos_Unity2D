using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcCaracter : MonoBehaviour
{
    public GameObject avaatar1, avatar2;

    int whichAvatarIsOn = 1;

    void Start()
    {
        avaatar1.gameObject.SetActive(true);
        avatar2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchAvatar()
    {
        switch (whichAvatarIsOn) 
        {
            case 1:

                whichAvatarIsOn = 2;
                
                avaatar1.gameObject.SetActive (false);
                avatar2.gameObject.SetActive (true);
                break;

            case 2:

                whichAvatarIsOn = 1;
                avaatar1.gameObject.SetActive (true);
                avatar2.gameObject.SetActive (false);
                break;

        }
    }
}
