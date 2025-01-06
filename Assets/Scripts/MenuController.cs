using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header ("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header ("Gameplay Settings")]
    [SerializeField] private TMP_Text ControllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 50;
    public int mainControllerSen = 50;

    [Header ("Graphics Settings")]
    private int _qualityLevel;
    private bool _isFullScreen;

    [Header ("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        _qualityLevel = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


   public void OnExitButtonPressed()
   {
    Application.Quit();
    Debug.Log("Hello: ");
   }

   public void SetVolume(float volume)//За регулиране силата на звука
   {
    AudioListener.volume = volume;
    volumeTextValue.text = volume.ToString("0.0");
   }

   public void VolumeApply()// Запазва силата на звука
   {
    PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    StartCoroutine(ConfirmationBox());
   }

   public void SetControllerSen(float sensitivity)
   {
    mainControllerSen = Mathf.RoundToInt(sensitivity);
    ControllerSenTextValue.text = sensitivity.ToString("0");
   }

   public void GameplayApply()
   {
    PlayerPrefs.SetFloat("masterSen", mainControllerSen);

    PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
    Screen.fullScreen = _isFullScreen;

    StartCoroutine(ConfirmationBox());
   }

   public void SetFullScreen(bool isFullScreen)
   {
    _isFullScreen = isFullScreen;
   }

   public void SetQuality(int qualityIndex)
   {
    _qualityLevel = qualityIndex;
   }

   public void GraphicsApply()
   {
    PlayerPrefs.SetInt("masterQuality", _qualityLevel);
    QualitySettings.SetQualityLevel(_qualityLevel);

    StartCoroutine(ConfirmationBox());
   }

   public void ResetButton(string MenuType)
   {
    if(MenuType == "Audio") 
    {
        AudioListener.volume = defaultVolume;
        volumeSlider.value = defaultVolume;
        volumeTextValue.text = defaultVolume.ToString("0.0");
        VolumeApply();
    }

    if(MenuType == "Gameplay")
    {
        ControllerSenTextValue.text = defaultSen.ToString("0");
        controllerSenSlider.value = defaultSen;
        mainControllerSen = defaultSen;
        GameplayApply();
    }
   }

   public IEnumerator ConfirmationBox()
   {
    confirmationPrompt.SetActive(true);
    yield return new WaitForSeconds(2);
    confirmationPrompt.SetActive(false);
   }
}
