using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeahtHole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallControler.setIsDeadTrue();
    }
}
