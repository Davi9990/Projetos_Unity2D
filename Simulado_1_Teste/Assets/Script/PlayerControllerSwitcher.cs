using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSwitcher : MonoBehaviour
{
    public MonoBehaviour controllerA; 
    public MonoBehaviour controllerB; 

    private CharacterController characterController;
    private Rigidbody rb;
    private bool usingControllerA = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        controllerA.enabled = true;
        controllerB.enabled = false;

        if (rb != null) rb.isKinematic = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            usingControllerA = !usingControllerA;

            controllerA.enabled = usingControllerA;
            controllerB.enabled = !usingControllerA;

            if (usingControllerA)
            {
                if (rb != null) rb.isKinematic = true;
                if (characterController != null) characterController.enabled = true;
            }
            else
            {
                if (rb == null)
                {
                    rb = gameObject.AddComponent<Rigidbody>();
                }

                rb.isKinematic = false;

                if (characterController != null) characterController.enabled = false;
            }
        }
    }
}
