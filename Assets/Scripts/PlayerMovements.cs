using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform Orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 5f; // За да спира играчът по-гладко
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //калкулира посоката на движение
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput; 

        if (moveDirection.magnitude > 0.1f)
        {   
        rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
        }
    }
}
