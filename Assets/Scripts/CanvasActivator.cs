using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public Canvas worldSpaceCanvas;   // Канвасът, който ще се показва и скрива
    public float activationDistance = 5f;  // Разстояние, от което канваса ще се активира
    private Camera playerCamera;  // Референция към главната камера на играча
    private bool isHovering = false;  // Проследява дали мишката е върху обекта

    void Start()
    {
        playerCamera = Camera.main;  // Взима главната камера
        if (worldSpaceCanvas != null)
        {
            worldSpaceCanvas.gameObject.SetActive(false);  // Скрива платното в началото
        }
    }

    void Update()
    {
        // Изчислява разстоянието между камерата на играча и обекта
        float distance = Vector3.Distance(playerCamera.transform.position, transform.position);
        bool isNear = distance <= activationDistance;  // Проверява дали играчът е в обхвата

        // Ако играчът е достатъчно близо
        if (isNear)
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // Изпраща лъч от камерата към позицията на мишката

            // Проверява дали лъчът удря някакъв колайдер на обект
            if (Physics.Raycast(ray, out hit))
            {
                // Ако удари обекта, на който е закачен скрипта 
                if (hit.transform == transform)
                {
                    isHovering = true;
                    worldSpaceCanvas.gameObject.SetActive(true);  // Показва платното
                }
                else
                {
                    isHovering = false;
                    worldSpaceCanvas.gameObject.SetActive(false);  // Скрива платното, ако мишката не е върху обекта
                }
            }
            else
            {
                isHovering = false;
                worldSpaceCanvas.gameObject.SetActive(false);  // Скрива платното, ако лъчът не удари колайдер
            }
        }
        else
        {
            isHovering = false;
            worldSpaceCanvas.gameObject.SetActive(false);  // Скрива платното, ако играчът не е наблизо
        }
    }
}
