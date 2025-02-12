using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class MenuController : MonoBehaviour
{
    [Header("Настройки за звука")] 
    [SerializeField] private TMP_Text volumeTextValue = null; // Променлива за текстовото поле, което показва нивото на звука
    [SerializeField] private Slider volumeSlider = null; // Променлива за плъзгача за силата на звука
    [SerializeField] private float defaultVolume = 0.5f; // Стойност по подразбиране за силата на звука
    [SerializeField] private GameObject confirmationPrompt = null; // Променлива за прозореца за потвърждение

    [Header("Настройки за геймплея")] 
    [SerializeField] private TMP_Text ControllerSenTextValue = null; // Променлива за текстовото поле, което показва чувствителността
    [SerializeField] private Slider controllerSenSlider = null; // Променлива за плъзгача за чувствителност
    [SerializeField] private int defaultSen = 50; // Стойност по подразбиране за чувствителността
    public int mainControllerSen = 50; // Променлива за съхранение на основната чувствителност

    [Header("Настройки за графика")] 
    private int _qualityLevel; // Променлива за нивото на качеството
    private bool _isFullScreen = true; // Променлива за запазване на текущото състояние на цял екран

    [Header("Падащо меню за резолюция")] 
    public TMP_Dropdown resolutionDropdown; // Променлива за падащото меню с резолюциите
    private Resolution[] resolutions; // Масив, съдържащ всички налични резолюции

    private void Start()
    {
        // Отключва курсора за главното меню
        CursorManager.SetCursorLockState(false);

        // Взима текущото ниво на качеството на графиката
        _qualityLevel = QualitySettings.GetQualityLevel();

        // Взима всички налични резолюции
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); 

        List<string> options = new List<string>(); // Списък за съхраняване на текстовите опции за резолюциите
        int currentResolutionIndex = 0; 

        // Цикъл за добавяне на всички налични резолюции в списъка
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Създава текст за всяка резолюция
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Ако текущата резолюция съвпада с настройката на екрана, я запазва
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Добавя опциите към падащото меню
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex; // Задаване на текущата резолюция като избрана
        resolutionDropdown.RefreshShownValue(); // Обновяване на визуализацията на падащото меню
    }

    // Функция за задаване на резолюцията
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex]; // Взима избраната резолюция от списъка
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); // Прилага новата резолюция
    }

    // Функция за изход от играта
    public void OnExitButtonPressed()
    {
        Application.Quit(); // Излиза от приложението
    }

    // Функция за задаване на нивото на звука
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Промяна на глобалното ниво на звука
        volumeTextValue.text = volume.ToString("0.0"); // Актуализиране на текстовото поле със стойността
    }

    // Запазване на настройките за звука
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume); // Запазва звука в настройките
        StartCoroutine(ConfirmationBox()); // Показва потвърдително съобщение
    }

    // Функция за задаване на чувствителността на контролера
    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity); // Закръгля стойността и записва
        ControllerSenTextValue.text = sensitivity.ToString("0"); // Обновява текстовото поле
    }

    //  За запазване на настройките за геймплея
    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("masterSen", mainControllerSen); // Запазва чувствителността

        // За запазване на настройките за цял екран
        if (_isFullScreen)
        {
            PlayerPrefs.SetInt("masterFullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterFullscreen", 0);
        }

        Screen.fullScreen = _isFullScreen; // Прилага настройката за цял екран
        StartCoroutine(ConfirmationBox()); // Показва потвърдително съобщение
    }

    // Функция за задаване на режим цял екран
    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen; // Запазва стойността в променлива
    }

    // Функция за задаване на графичното качество
    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex; // Запазва избраното ниво на качество
    }

    // За запазване на настройките за графика
    public void GraphicsApply()
    {
        PlayerPrefs.SetInt("masterQuality", _qualityLevel); // Запазва качеството в настройките
        QualitySettings.SetQualityLevel(_qualityLevel); // Прилага настройките за качество
        StartCoroutine(ConfirmationBox()); // Показване потвърдително съобщение
    }

    // Функция за нулиране на настройките
    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            // Нулира звука до стойност по подразбиране
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {
            // Нулира  чувствителността до стойност по подразбиране
            ControllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            GameplayApply();
        }
    }

    // Корутина за показване на потвърдително съобщение
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true); // Показва прозореца за потвърждение
        yield return new WaitForSeconds(2); // Изчаква 2 секунди
        confirmationPrompt.SetActive(false); // Скрива прозореца
    }
}
