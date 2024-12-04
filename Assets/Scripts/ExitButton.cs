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
    [SerializeField] private GameObject confirmationPrompt = null;

   public void OnExitButtonPressed()
   {
    Application.Quit();
    Debug.Log("Hello: ");
   }

   public void SetVolume(float volume)//За регулиране силата на звука
   {
    AudioListener.volume = volume;
    volumeTextValue.text = volume.ToString("0");
   }

   public void VolumeApply()// Запазва силата на звука
   {
    PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    StartCoroutine(ConfirmationBox());

   }

   public IEnumerator ConfirmationBox()
   {
    confirmationPrompt.SetActive(true);
    yield return new WaitForSeconds(2);
    confirmationPrompt.SetActive(false);
   }
}
