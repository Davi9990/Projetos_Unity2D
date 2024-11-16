using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHole : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallControler.setYouWinToTrue();
    }
}
