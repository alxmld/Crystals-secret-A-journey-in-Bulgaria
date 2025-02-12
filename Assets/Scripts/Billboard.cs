using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float hoverDistance = 5f;  // Дистанция, от която спрайта ще се показва
    [SerializeField] private GameObject targetObject;  // За обекта, над който ще излиза спрайта

    public enum BillboardType { LookAtCamera, CameraForward };

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer if not set
        }
    }

    void Update()
    {
        // Check if the player is within hover distance and is hovering over the target object
        if (targetObject != null && Vector3.Distance(targetObject.transform.position, Camera.main.transform.position) <= hoverDistance)
        {
            // Perform a raycast to detect if the player is hovering over the target object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == targetObject)
            {
                // If player is hovering over the target object, show the sprite and make it follow the camera
                spriteRenderer.enabled = true;

                // Make sprite face the camera
                switch (billboardType)
                {
                    case BillboardType.LookAtCamera:
                        transform.LookAt(Camera.main.transform.position, Vector3.up);
                        break;
                    case BillboardType.CameraForward:
                        transform.forward = Camera.main.transform.forward;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // If the player is not hovering over the target object, hide the sprite
                spriteRenderer.enabled = false;
            }
        }
        else
        {
            // If player is not close enough, hide the sprite
            spriteRenderer.enabled = false;
        }
    }
}
