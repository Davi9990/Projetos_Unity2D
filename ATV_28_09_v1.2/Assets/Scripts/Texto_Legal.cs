using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Texto_Legal : MonoBehaviour
{
    public Vector2 startPos, direction;

    public TextMeshProUGUI m_Text;
    string message;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = "Touch: " +message+ "in direction" + direction;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    message = "Began ";
                    break;

                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    message = "Moving ";
                    break;

                case TouchPhase.Ended:
                    message = "Ending ";
                    break;
            }
        }
    }
}
