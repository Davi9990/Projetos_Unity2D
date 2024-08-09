using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerManeger : MonoBehaviour
{
    public GameObject originalPrefab;
    private GameObject currentPrefab;

    void Start()
    {
        currentPrefab = gameObject;
    }

    public void UpgradePlayer(GameObject newPrefab)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        GameObject upgradedPlayer = Instantiate(newPrefab, position, rotation);
        CopyComponents(gameObject, upgradedPlayer);

        Destroy(gameObject);
        currentPrefab = upgradedPlayer;
    }

    public void RevertPlayer()
    {
        Vector3 position = currentPrefab.transform.position;
        Quaternion rotation = currentPrefab.transform.rotation;

        GameObject revertedPlayer = Instantiate(originalPrefab, position, rotation);
        CopyComponents(currentPrefab, revertedPlayer);

        Destroy(currentPrefab);
        currentPrefab = revertedPlayer;
    }

    private void CopyComponents(GameObject source, GameObject destination)
    {
        foreach (Component component in source.GetComponents<Component>())
        {
            if (component is Transform || component is Animator)
                continue;

            System.Type type = component.GetType();

            // Verifica se o componente já existe
            Component existingComponent = destination.GetComponent(type);
            if (existingComponent == null)
            {
                Component copy = destination.AddComponent(type);

                foreach (var field in type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
                {
                    field.SetValue(copy, field.GetValue(component));
                }
            }
            else
            {
                // Copia os valores dos campos para o componente existente
                foreach (var field in type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
                {
                    field.SetValue(existingComponent, field.GetValue(component));
                }
            }
        }

        // Substitui o Animator com o do novo prefab
        Animator newAnimator = destination.GetComponent<Animator>();
        if (newAnimator == null)
        {
            Animator oldAnimator = source.GetComponent<Animator>();
            newAnimator = destination.AddComponent<Animator>();
            newAnimator.runtimeAnimatorController = oldAnimator.runtimeAnimatorController;
        }
    }
}
