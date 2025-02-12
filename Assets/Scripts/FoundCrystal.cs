using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundCrystal : MonoBehaviour
{
    public GameObject crystal; // Assign the crystal object in the Inspector
    public GameObject crystalWindow; // Assign the UI Canvas in the Inspector
    public float interactionDistance = 3.0f; // Max distance to interact with the crystal

    public Camera mainCamera; // Assign the main gameplay camera
    public Camera uiCamera; // Assign the UI camera (the one used for the Canvas)

    public GameObject[] objectsToDisable; // Array of objects to disable when UI is active
    public Button homeButton;

    void Awake()
    {
        if (crystal == null)
        {
            Debug.LogError("Crystal GameObject is not assigned. Please assign it in the Inspector.");
        }
        if (crystalWindow == null)
        {
            Debug.LogError("UI component is not assigned. Please assign it in the Inspector.");
        }
        else
        {
            crystalWindow.SetActive(false); // Hide the UI at the start
        }

        if (mainCamera == null || uiCamera == null)
        {
            Debug.LogError("Cameras are not assigned. Please assign them in the Inspector.");
        }
        else
        {
            uiCamera.gameObject.SetActive(false); // Ensure UI camera starts disabled
        }
    }

    void Update()
    {
        // Detect interaction key press
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed!");

            // Check if the player is near and looking at the crystal
            if (IsPlayerNearAndLookingAtCrystal())
            {
                Debug.Log("Player is near and looking at the crystal. Showing UI.");
                ShowCanvas();
            }
            else
            {
                Debug.Log("Player is either too far or not looking at the crystal.");
            }
        }
    }

    private bool IsPlayerNearAndLookingAtCrystal()
    {
        if (crystal == null || Camera.main == null)
        {
            return false;
        }

        // Check distance
        float distance = Vector3.Distance(crystal.transform.position, Camera.main.transform.position);
        if (distance > interactionDistance) return false;

        // Check if the cursor is over the crystal using raycasting
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == crystal)
            {
                Debug.Log("Cursor is over the crystal.");
                return true;
            }
        }

        return false;
    }

    private void ShowCanvas()
    {
        if (crystalWindow != null)
        {
            crystalWindow.SetActive(true); // Show the UI
            Debug.Log("Crystal UI is now visible.");

            // Switch cameras
            if (mainCamera != null) mainCamera.gameObject.SetActive(false);
            if (uiCamera != null) uiCamera.gameObject.SetActive(true);

            // Disable specified objects
            SetObjectsActive(objectsToDisable, false);

            // Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("No UI Canvas assigned.");
        }
    }

    // Function to close the UI window
    public void HideCanvas()
    {
        if (crystalWindow != null)
        {
            crystalWindow.SetActive(false); // Hide the UI
            Debug.Log("Crystal UI is now hidden.");

            // Switch cameras back
            if (mainCamera != null) mainCamera.gameObject.SetActive(true);
            if (uiCamera != null) uiCamera.gameObject.SetActive(false);

            // Re-enable specified objects
            SetObjectsActive(objectsToDisable, true);

            // Lock the cursor back
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            homeButton.interactable = true;
        }
    }

    // Helper function to enable/disable multiple GameObjects
    private void SetObjectsActive(GameObject[] objects, bool isActive)
    {
        if (objects == null) return;

        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }
}
