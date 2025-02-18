using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavesBookOpen : MonoBehaviour
{
    public GameObject GameObject; // Обектът, който ще се активира (UI прозорец)
    public GameObject[] caves; // Масив от обекти, които са пещери

    void Update()
    {
        //  Проверка дали е натиснат клавишът "E" за взаимодействие
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Обхожда масива от пещери, за да провери дали някоя е активна
            foreach (GameObject cave in caves)
            {
                if (cave.activeSelf) // Ако някоя от пещерите в масива е активна
                {
                    // Активира UI прозореца
                    GameObject.SetActive(true);

                    // Отключва и показва курсора
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // Прекъсва цикъла ако даденото условие е изпълнено
                    break;
                }
            }
        }
    }
}
