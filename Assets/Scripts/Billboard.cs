using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float hoverDistance = 5f;  // Разстояние, на което спрайтът ще се показва

    [SerializeField] private GameObject targetObject;  // Обектът, над който трябва да се появява спрайтът

    public enum BillboardType { LookAtCamera, CameraForward };

    private void Start()
    {
        // Ако spriteRenderer не е зададен в инспектора, взимаме компонента от текущия обект
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();  
        }
    }

    void Update()
    {
        // Проверява дали играчът е в допустимата дистанция и дали се намира над targetObject
        if (targetObject != null && Vector3.Distance(targetObject.transform.position, Camera.main.transform.position) <= hoverDistance)
        {
            // Създава лъч (raycast) от позицията на мишката по посока на камерата
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверява дали лъчът удря targetObject
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == targetObject)
            {
                // Ако мишката е над обекта, показваме спрайта
                spriteRenderer.enabled = true;

                // Завъртаме спрайта спрямо камерата според избрания тип билборд
                switch (billboardType)
                {
                    case BillboardType.LookAtCamera:
                        transform.LookAt(Camera.main.transform.position, Vector3.up); // Обектът гледа към камерата
                        break;
                    case BillboardType.CameraForward:
                        transform.forward = Camera.main.transform.forward; // Обектът следва посоката на камерата
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // Ако мишката не е над обекта, скриваме спрайта
                spriteRenderer.enabled = false;
            }
        }
        else
        {
            // Ако играчът е твърде далеч, скриваме спрайта
            spriteRenderer.enabled = false;
        }
    }
}
