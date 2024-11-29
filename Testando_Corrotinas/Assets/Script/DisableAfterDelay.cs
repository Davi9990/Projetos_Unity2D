using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DisableObject(3f));
    }
    IEnumerator DisableObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Desativa o objeto
    }

}
