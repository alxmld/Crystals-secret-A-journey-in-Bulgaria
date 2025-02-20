using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // За UI на менюто за пауза
    [SerializeField] private GameObject[] restrictedObjects; // Масив от обекти, които ще блокират менюто за пауза
    public GameObject[] crosshairs; // Масив за мерници
    private bool isPaused = false; // Проследява дали играта е на пауза

    [SerializeField] private CavesBookOpen cavesBookOpenScript; // Референция към CavesBookOpen скрипта

    private void Update()
    {
        // Проверява дали е натиснат клавишът "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Ако някой от обектите в масива е активен, не позволява изпълнение
            if (IsAnyRestrictedObjectActive())
            {
                return; // Спира изпълнението на кода
            }

            // Превключва състоянието на паузата
            isPaused = !isPaused;

            // Ако играта е на пауза, активира менюто за пауза, иначе го деактивира
            if (isPaused)
                ActivateMenu();
            else
                DeactivateMenu();
        }
    }

    // Проверява дали някой от ограничените обекти е активен
    private bool IsAnyRestrictedObjectActive()
    {
        foreach (GameObject obj in restrictedObjects)
        {
            if (obj.activeInHierarchy) // Ако обектът е активен в йерархията на сцената
                return true; // Връща true, което означава, че менюто за пауза не може да се активира
        }
        return false; // Ако няма активни ограничени обекти, връща false
    }

    // Активира менюто за пауза
    private void ActivateMenu()
    {
        Time.timeScale = 0; // Спира времето в играта
        AudioListener.pause = true; // Спира звука
        pauseMenuUI.SetActive(true); // Показва UI на менюто за пауза
        CursorManager.SetCursorLockState(false); // Отключва и показва курсора на мишката
        SetCrosshairsActive(false); // Скрива мерниците

        // Деактивира CavesBookOpen скрипта
        if (cavesBookOpenScript != null)
        {
            cavesBookOpenScript.enabled = false;
        }
    }

    // Деактивира менюто за пауза
    public void DeactivateMenu()
    {
        Time.timeScale = 1; // Възобновява времето в играта
        AudioListener.pause = false; // Възобновява звука
        pauseMenuUI.SetActive(false); // Скрива UI менюто за пауза
        isPaused = false; // Задава флага за пауза на false
        CursorManager.SetCursorLockState(true); // Заключва и скрива курсора на мишката
        SetCrosshairsActive(true); // Показва мерниците

        // Активира CavesBookOpen скрипта
        if (cavesBookOpenScript != null)
        {
            cavesBookOpenScript.enabled = true;
        }
    }

    // Функция за изход от играта
    public void QuitGame()
    {
        Application.Quit(); // Затваря играта
    }

    // Функция за активиране/деактивиране на всички мерници
    private void SetCrosshairsActive(bool state)
    {
        foreach (GameObject crosshair in crosshairs)
        {
            crosshair.SetActive(state);
        }
    }
}