using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public Canvas worldSpaceCanvas;   // Assign the Canvas in the Inspector
    public float activationDistance = 5f;  // Distance from the object where the canvas will be activated

    private Camera playerCamera;
    private bool isHovering = false;

    void Start()
    {
        playerCamera = Camera.main;
        if (worldSpaceCanvas != null)
        {
            worldSpaceCanvas.gameObject.SetActive(false);  // Start with canvas hidden
        }
    }

    void Update()
    {
        // Check if the player is within activation distance
        float distance = Vector3.Distance(playerCamera.transform.position, transform.position);
        bool isNear = distance <= activationDistance;

        // Only proceed if the player is close enough to the object
        if (isNear)
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // Ray from camera to mouse position

            // Check if the ray hits the object (this should be the object with the script attached)
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)  // Check if the ray hits the same object
                {
                    isHovering = true;
                    worldSpaceCanvas.gameObject.SetActive(true);  // Show the canvas when hovering
                }
                else
                {
                    isHovering = false;
                    worldSpaceCanvas.gameObject.SetActive(false);  // Hide canvas when not hovering
                }
            }
            else
            {
                isHovering = false;
                worldSpaceCanvas.gameObject.SetActive(false);  // Hide canvas when ray doesn't hit
            }
        }
        else
        {
            isHovering = false;
            worldSpaceCanvas.gameObject.SetActive(false);  // Hide canvas when not near
        }
    }
}
