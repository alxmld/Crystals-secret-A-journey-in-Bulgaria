using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f; // За бързината за въртене на обекта
    [SerializeField] bool isDragging = false; // Задава, че няма задържане на левия бутон и преместване на мишката
    [SerializeField] Rigidbody rb; // За обекта с Rigidbody компонент, който ще въртим

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Извиква се при задържане на левия бутон и преместване на мишката
    void OnMouseDrag()
    {
        isDragging = true; // Задава, че има задържане на левия бутон и преместване на мишката
    }

    void Update()
    {
        // Ако левия бутон на мишката е отпуснат
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false; // Задава, че няма задържане на левия бутон и преместване на мишката
        }
    }

    // Извиква се при фиксиран интервал за физически пресмятания
    void FixedUpdate()
    {
        // Ако има задържане на левия бутон и преместване на мишката
        if (isDragging)
        {
            // Изчислява въртенето и прилага на торк
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            rb.AddTorque(Vector3.down * x);
        }
    }
}