using UnityEngine;


// Скрипт за въртене на обект с мишката
public class RotateObject : MonoBehaviour
{
    //--------- ПРОМЕНЛИВИ и ВРЪЗКИ ---------
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] bool isDragging = false;
    [SerializeField] Rigidbody rb;


    //--------- ФУНКЦИИ ---------
    // Вика се в началото на играта и създава връзка между скрипта и компонента
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Извиква се при задържане на бутона на мишката
    void OnMouseDrag()
    {
        isDragging = true;
    }

    // Извиква се при всяко обновление на сцената
    void Update()
    {
        // Проверка дали бутона на мишката е отпуснат
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    // Извиква се при фиксиран интервал за физически пресмятания
    void FixedUpdate()
    {
        // Проверка дали се задържа бутона на мишката
        if (isDragging)
        {
            // Изчисляване на въртенето и прилагане на торк
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            rb.AddTorque(Vector3.down * x);
        }
    }
}