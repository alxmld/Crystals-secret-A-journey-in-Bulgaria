using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Интерфейс за интерактивни обекти
interface IInteractable 
{
    public void Interact(); // Метод, който ще бъде имплементиран от интерактивните обекти
}

// Клас, отговарящ за взаимодействието с интерактивни обекти
public class Interaction : MonoBehaviour
{
    public Transform InteractorSource; // Трансформ на обекта, който ще инициира интеракцията
    public float InteractRange; // Максимално разстояние за интерактване

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Ако "E" е натиснат
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward); // Създава Ray от позицията на InteractorSource напред
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)) // Проверява дали лъчът удря обект в обсега
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) // Проверява дали обектът има компонент, който имплементира IInteractable
                {
                    interactObj.Interact(); // Извиква метода Interact на намерения обект
                }
            }
        }
    }
}
