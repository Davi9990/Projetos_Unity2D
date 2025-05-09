using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public enum CameraState { Normal, Passage }
    public CameraState targetState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraStateManager.Instance.SetState(targetState);
        }
    }
}
