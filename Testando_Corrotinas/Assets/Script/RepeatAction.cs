using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatAction : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(RepeatMessage(1f));
    }
    IEnumerator RepeatMessage(float interval)
    {
        while (true)
        {
            Debug.Log("A��o repetida!");
            yield return new WaitForSeconds(interval);
        }
    }
}
