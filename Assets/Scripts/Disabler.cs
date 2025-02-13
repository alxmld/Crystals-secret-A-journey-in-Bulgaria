using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject[] targetButtons; // Array of UI Buttons to enable/disable
    public GameObject roomGameObject;  // The GameObject that controls button visibility

    void Update()
    {
        if (roomGameObject == null || targetButtons == null) return;

        bool isOtherActive = roomGameObject.activeSelf;

        // Iterate through all buttons and enable/disable them
        foreach (GameObject button in targetButtons)
        {
            if (button != null)
            {
                button.SetActive(!isOtherActive);
            }
        }
    }
}
