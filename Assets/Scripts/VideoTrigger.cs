using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VideoTrigger : MonoBehaviour
{
    public GameObject book; // Assign the book object in the Inspector

    void Awake()
    {
        if (book == null)
        {
            Debug.LogError("Book GameObject is not assigned. Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        // Check for mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button down detected!");

            // Check if the raycast hits the book
            if (book != null && book == GetClickedObject(out RaycastHit hit))
            {
                Debug.Log("Clicked/Touched the book!");
            }
            else
            {
                Debug.Log("Click missed the book.");
            }
        }
    }

    GameObject GetClickedObject(out RaycastHit hit)
    {
        hit = default;
        GameObject target = null;

        if (Camera.main == null)
        {
            Debug.LogError("MainCamera not found. Ensure the camera is tagged as 'MainCamera'.");
            return null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check raycast
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log($"Ray hit object: {hit.collider.gameObject.name}");
            if (!IsPointerOverUIObject()) // Ensure UI doesn’t block the detection
            {
                target = hit.collider.gameObject;
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any object.");
        }

        return target;
    }

    private bool IsPointerOverUIObject()
    {
        // Ensure EventSystem exists
        if (EventSystem.current == null)
        {
            Debug.LogError("EventSystem not found. Ensure you have an EventSystem in the scene.");
            return false;
        }

        PointerEventData ped = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);

        bool isOverUI = results.Count > 0;

        if (isOverUI)
        {
            Debug.Log("Pointer is over a UI element.");
        }

        return isOverUI;
    }
}
