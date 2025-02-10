using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Препратка към обекта, който определя позицията на камерата
    public Transform cameraPosition;
    private void Update()
    {
       // Постоянно задава позицията на този обект да съвпада с позицията на cameraPosition 
       transform.position = cameraPosition.position; 
    }
}
