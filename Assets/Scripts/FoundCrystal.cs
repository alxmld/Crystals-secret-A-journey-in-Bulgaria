using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundCrystal : MonoBehaviour
{
    public GameObject crystal; // Kристалът, който ще се открива
    public GameObject crystalWindow; // UI прозорецът, който ще се показва когато кристал е открит
    public float interactionDistance = 3.0f;  // Максимално разстояние, на което играчът може да интерактва с кристала
    public Camera mainCamera; // Главната камера на играча
    public Camera uiCamera; // Камерата за UI елементи
    public GameObject[] objectsToDisable; // Масив от обекти, които ще се изключват при показване на UI
    public Button homeButton; // Бутон за връщане към началния екран

    void Awake()
    {
        // Проверка дали кристалът е зададен
        if (crystal == null)
        {
            Debug.LogError("Crystal GameObject is not assigned. Please assign it in the Inspector.");
        }

        // Проверка дали UI прозорецът е зададен
        if (crystalWindow == null)
        {
            Debug.LogError("UI component is not assigned. Please assign it in the Inspector.");
        }
        else
        {
            crystalWindow.SetActive(false); // Скриване на UI в началото
        }

        // Проверка дали камерите са зададени
        if (mainCamera == null || uiCamera == null)
        {
            Debug.LogError("Cameras are not assigned. Please assign them in the Inspector.");
        }
        else
        {
            uiCamera.gameObject.SetActive(false); // Уверяваме се, че UI камерата е изключена в началото
        }
    }

    void Update()
    {
        // Проверка дали е натиснат клавишът "R" за взаимодействие
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed!");

            // Проверка дали играчът е достатъчно близо и гледа към кристала
            if (IsPlayerNearAndLookingAtCrystal())
            {
                Debug.Log("Player is near and looking at the crystal. Showing UI.");
                ShowCanvas(); // Показване на UI прозореца
            }
            else
            {
                Debug.Log("Player is either too far or not looking at the crystal.");
            }
        }
    }

    // Функция за проверка дали играчът е в обхват и гледа към кристала
    private bool IsPlayerNearAndLookingAtCrystal()
    {
        if (crystal == null || Camera.main == null)
        {
            return false;
        }

        // Проверка на разстоянието между играча и кристала
        float distance = Vector3.Distance(crystal.transform.position, Camera.main.transform.position);
        if (distance > interactionDistance) return false;

        // Проверка дали курсорът е над кристала с помощта на Raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == crystal)
            {
                Debug.Log("Cursor is over the crystal.");
                return true;
            }
        }

        return false;
    }

    // Функция за показване на UI прозореца
    private void ShowCanvas()
    {
        if (crystalWindow != null)
        {
            crystalWindow.SetActive(true); // Активира UI прозореца
            Debug.Log("Crystal UI is now visible.");

            // Превключва камерите
            if (mainCamera != null) mainCamera.gameObject.SetActive(false);
            if (uiCamera != null) uiCamera.gameObject.SetActive(true);

            // Деактивира определени обекти
            SetObjectsActive(objectsToDisable, false);

            // Отключва и показва курсора
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("No UI Canvas assigned.");
        }
    }

    // Функция за скриване на UI прозореца
    public void HideCanvas()
    {
        if (crystalWindow != null)
        {
            crystalWindow.SetActive(false); // Скрива UI прозореца
            Debug.Log("Crystal UI is now hidden.");

            // Връща към основната камера
            if (mainCamera != null) mainCamera.gameObject.SetActive(true);
            if (uiCamera != null) uiCamera.gameObject.SetActive(false);

            // Реактивиране на обектите
            SetObjectsActive(objectsToDisable, true);

            // Заключва и скрива на курсора
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Активира бутона за връщане към началния екран
            homeButton.interactable = true;
        }
    }

    // Помощна функция за активиране/деактивиране на масив от обекти
    private void SetObjectsActive(GameObject[] objects, bool isActive)
    {
        if (objects == null) return;

        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }
}
