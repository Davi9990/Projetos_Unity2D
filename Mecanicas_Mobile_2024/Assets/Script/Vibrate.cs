using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour
{
    private void OnMouseDown()
    {
        Handheld.Vibrate();
    }
}
