using UnityEngine;
using UnityEngine.UI;

public class GameObjectStateWatcher : MonoBehaviour
{
    public GameObject prevFoundCrystalCanvas; // Канвас, който вече не трябва да е активен
    public GameObject room; // Обектът, който трябва да е активен в момента
    public Button[] targetButtons; // Масив от бутони за активиране
    public GameObject[] targetImages; // Масив от изображения за скриване

    private bool prevFoundCrystalCanvasWasActive = false; // Задава, че канвасите все още не са открити

    void Update()
    {
        // Проверява дали вървия обект е бил активен и вече не е
        if (prevFoundCrystalCanvas.activeSelf)
        {
            prevFoundCrystalCanvasWasActive = true; // Ако вече не е връща true
        }

        // Ако първия обект, в случар канваса, е бил активен и вече не е и втория обект е активен
        if (prevFoundCrystalCanvasWasActive && !prevFoundCrystalCanvas.activeSelf && room.activeSelf)
        {
            // Активира всички бутони в масива
            foreach (Button btn in targetButtons)
            {
                btn.interactable = true;
            }

            // Скрива всички изображения в масива
            foreach (GameObject img in targetImages)
            {
                img.SetActive(false);
            }

            // Деактивира скрипта
            enabled = false;
        }
    }
}