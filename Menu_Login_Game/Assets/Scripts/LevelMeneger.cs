using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMeneger : MonoBehaviour
{
    public GameObject[] ovos; // Array de ovos
    public GameObject[] targetAreas; // Array de �reas-alvo

    void Update()
    {
        if (CheckAllOvosInPlace())
        {
            // Todos os ovos est�o nas �reas corretas, passar para a pr�xima fase
            GoToNextLevel();
        }
    }

    private bool CheckAllOvosInPlace()
    {
        foreach (GameObject ovo in ovos)
        {
            bool ovoInPlace = false;
            foreach (GameObject target in targetAreas)
            {
                if (Vector3.Distance(ovo.transform.position, target.transform.position) < 0.5f)
                {
                    ovoInPlace = true;
                    break;
                }
            }
            if (!ovoInPlace)
            {
                return false;
            }
        }
        return true;
    }

    private void GoToNextLevel()
    {
        // L�gica para ir para a pr�xima fase
        Debug.Log("Passou para a pr�xima fase!");
        // Exemplo: SceneManager.LoadScene("NextLevelScene");
    }
}
