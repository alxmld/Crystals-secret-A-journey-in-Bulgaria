using UnityEngine;
using UnityEngine.UI;

public class GameObjectStateWatcher : MonoBehaviour
{
    public GameObject firstGameObject;   // The one that should be inactive
    public GameObject secondGameObject;  // The one that should be active
    public Button[] targetButtons;       // Array of buttons to enable
    public GameObject[] targetImages;    // Array of images to hide

    private bool firstObjectWasActive = false;

    void Update()
    {
        // Check if the first object was active and is now inactive
        if (firstGameObject.activeSelf)
        {
            firstObjectWasActive = true;
        }

        // If the first object was active but is now inactive, and the second object is active
        if (firstObjectWasActive && !firstGameObject.activeSelf && secondGameObject.activeSelf)
        {
            // Enable all buttons in the array
            foreach (Button btn in targetButtons)
            {
                btn.interactable = true;
            }

            // Hide all images in the array
            foreach (GameObject img in targetImages)
            {
                img.SetActive(false);
            }

            // Optionally, disable this script if no longer needed
            enabled = false;
        }
    }
}