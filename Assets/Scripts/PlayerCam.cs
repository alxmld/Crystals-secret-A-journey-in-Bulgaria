using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    // Чувствителност на мишката по осите X и Y
    public float sensX;
    public float sensY;

    // Чувствителност на движението на камерата
    public float smoothing = 10f;

    // Препратка към ориентацията на играча
    public Transform orientation;

    float xRotation;
    float yRotation;
    
    private void Start()
    {
        // Заключва курсора в центъра на екрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update ()
    {
       float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
       float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

       //Актуализиране на въртенето по Y
       yRotation += mouseX;

       //Актуализиране на въртенето по X и ограничаване на ъгъла от -90 до 90 градуса
       xRotation -= mouseY;
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);

       // Завъртане на камерата по X и Y
       transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
       // Завъртане на ориентацията само по Y, за да не накланя играча
       orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}