using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject targetButton; // Assign the UI Button in the Inspector
    public GameObject roomGameObject; // Assign the GameObject that should disable the button

    void Update()
    {
        if (targetButton != null && roomGameObject != null)
        {
            bool isOtherActive = roomGameObject.activeSelf;

            // Disable targetButton only when roomGameObject is active
            targetButton.SetActive(!isOtherActive);
        }
    }
}
