using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Назначете вашето UI меню за пауза в инспектора
    [SerializeField] private GameObject[] restrictedObjects; // Масив от обекти, които ще блокират менюто за пауза
    private bool isPaused = false; // Проследява дали играта е на пауза

    private void Update()
    {
        // Проверява дали е натиснат клавишът "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Ако някой от обектите в масива е активен, не позволява изпълнение на паузата
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
        pauseMenuUI.SetActive(true); // Показва UI менюто за пауза
        CursorManager.SetCursorLockState(false); // Отключва и показва курсора на мишката
    }

    // Деактивира менюто за пауза
    public void DeactivateMenu()
    {
        Time.timeScale = 1; // Възобновява времето в играта
        AudioListener.pause = false; // Възобновява звука
        pauseMenuUI.SetActive(false); // Скрива UI менюто за пауза
        isPaused = false; // Задава флага за пауза на false
        CursorManager.SetCursorLockState(true); // Заключва и скрива курсора на мишката
    }

    // Функция за изход от играта
    public void QuitGame()
    {
        Application.Quit(); // Затваря играта
    }
}
