using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateManager : MonoBehaviour
{
    public static CameraStateManager Instance;

    public Cinemachine.CinemachineVirtualCamera followCamera;
    public Cinemachine.CinemachineVirtualCamera passageCamera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetState(CameraTrigger.CameraState state)
    {
        switch (state)
        {
            case CameraTrigger.CameraState.Normal:
                followCamera.Priority = 10;
                passageCamera.Priority = 5;
                break;
            case CameraTrigger.CameraState.Passage:
                followCamera.Priority = 5;
                passageCamera.Priority = 10;
                break;
        }
    }
}
