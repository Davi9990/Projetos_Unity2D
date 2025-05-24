using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject followCamera;
    public GameObject dollyCamera;

    private bool usingFollow = true;

    void Start()
    {
        followCamera.SetActive(true);
        dollyCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            usingFollow = !usingFollow;
            followCamera.SetActive(usingFollow);
            dollyCamera.SetActive(!usingFollow);
        }
    }
}
