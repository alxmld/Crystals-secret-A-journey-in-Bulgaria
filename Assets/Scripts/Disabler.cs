using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject[] targetButtons; // Масив от бутони, които ще бъдат активирани или деактивирани

    public GameObject roomGameObject;  // Обектът, чиято активност ще определя състоянието на бутоните

    void Update()
    {
        // Ако roomGameObject или targetButtons не са зададени, излиза от функцията
        if (roomGameObject == null || targetButtons == null) return;

        // Проверява дали roomGameObject е активен
        bool isOtherActive = roomGameObject.activeSelf;

        // За aктивиране/деактивиране на бутоните в масива
        foreach (GameObject button in targetButtons)
        {
            if (button != null)
            {
                // Ако roomGameObject е активен, бутоните ще бъдат деактивирани и обратно
                button.SetActive(!isOtherActive);
            }
        }
    }
}
