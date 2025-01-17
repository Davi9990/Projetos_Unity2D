using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGroup : MonoBehaviour
{
    private HorizontalLayoutGroup horizontalLayoutGroup;

    void Start()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    public void DisableLayout()
    {
        if (horizontalLayoutGroup == null)
        {
            horizontalLayoutGroup.enabled = false;
        }
    }

    public void EnableLayout()
    {
        if (horizontalLayoutGroup != null)
        {
            horizontalLayoutGroup.enabled = true;
        }
    }
}
