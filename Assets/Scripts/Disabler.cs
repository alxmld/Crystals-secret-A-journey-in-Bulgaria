using UnityEngine;
using UnityEngine.UI;

public class Disabler : MonoBehaviour
{
    public Button targetButton; // Assign the UI Button in the Inspector
    public GameObject otherGameObject; // Assign the GameObject that should disable the button

    void Update()
    {
        if (targetButton != null && otherGameObject != null)
        {
            targetButton.interactable = !otherGameObject.activeSelf;
        }
    }
}
