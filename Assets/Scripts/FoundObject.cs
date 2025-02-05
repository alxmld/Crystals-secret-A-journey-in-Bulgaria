using UnityEngine;

public class FoundObject : MonoBehaviour
{
    public GameObject targetObject;  // The object the player needs to find
    public GameObject uiElement;     // The UI that should appear

    private bool objectFound = false;

    void Update()
    {
        // Check if the player finds the target object
        if (Vector3.Distance(transform.position, targetObject.transform.position) < 2f) 
        {
            objectFound = true;  // Object is found if close enough
        }

        // If object is found and the player presses a key, show the UI
        if (objectFound && Input.GetKeyDown(KeyCode.E)) 
        {
            uiElement.SetActive(true);
        }
    }
}
