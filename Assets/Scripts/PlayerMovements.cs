using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // За скорост на движението на играча

    public Transform Orientation; // Ориентацията на играча

    float horizontalInput; // Вход за хоризонтално движение
    float verticalInput; // Вход за вертикално движение

    Vector3 moveDirection; // Вектор за посоката на движение

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Заключване на ротацията, за да не пада или се върти неочаквано
        rb.drag = 5f; // Добавяне на съпротивление, за да може играчът да спира по-гладко
    }

    private void Update()
    {
        MyInput(); // Обработване на входа от клавиатурата
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        // Взима входа от клавиатурата
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Калкулира посоката на движение спрямо ориентацията на играча
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput; 

        if (moveDirection.magnitude > 0.1f)
        {   
        // Добавя сила към Rigidbody, за да придвижи играча в желаната посока        
        rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
        }
    }
}
