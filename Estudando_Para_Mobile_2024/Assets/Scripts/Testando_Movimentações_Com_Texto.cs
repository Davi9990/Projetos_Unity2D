using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;

public class Testando_Movimentações_Com_Texto : MonoBehaviour
{
    public Vector2 startPos, direction;

    public TextMeshProUGUI Text;
    string Message;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Touch : " + Message + "in direction" + direction;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    Message = "Began ";
                    break;

                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    Message = "Moving";
                    break;

                case TouchPhase.Ended:
                    Message = "Ending";
                    break;
            }
        }
    }
}
