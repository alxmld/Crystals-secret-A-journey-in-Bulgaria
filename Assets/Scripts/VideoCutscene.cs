using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoCutscene : MonoBehaviour, IInteractable
{
    public VideoPlayer videoPlayer; // Video player, който възпроизвежда видеото
    public GameObject videoCanvas; // Канвас, който показва видеото
    public GameObject bookCanvas; // Канвас, който показва енциклопедията след края на видеото
    public Button closeButton; // Бутонът, който затваря енциклопедията

    // Методът Interact се извиква, когато играчът взаимодейства с обекта
    public void Interact()
    {
        PlayCutscene(); // Стартира възпроизвеждането на кътсцената
    }

    // Метод за стартиране на кътсцената
    private void PlayCutscene()
    {
        // Проверява дали VideoPlayer и videoCanvas са налични
        if (videoPlayer == null || videoCanvas == null)
        {
            Debug.LogWarning("Липсва референция към VideoPlayer или VideoCanvas!");
            return;
        }

        videoPlayer.Stop(); // Спира видеото, ако е пуснато
        videoPlayer.frame = 0; // Връща видеото към първия кадър

        // Изчиства Render Texture, за да не се показва последния кадър от кътсцената
        if (videoPlayer.targetTexture != null)
        {
            RenderTexture renderTexture = videoPlayer.targetTexture;
            RenderTexture.active = renderTexture;
            GL.Clear(true, true, Color.black); // Изчиства текстурата с черен фон
            RenderTexture.active = null;
        }

        videoCanvas.SetActive(true); // Активира канваса, който показва видеото

        Time.timeScale = 0; // Спира времето в играта, докато видеото се възпроизвежда

        // Добавяне на слушател за събитието loopPointReached, което се извиква, когато видеото приключи
        videoPlayer.loopPointReached -= EndCutscene; // Премахване на слушателя, за да се избегне дублиране
        videoPlayer.loopPointReached += EndCutscene;

        videoPlayer.Play(); // Пуска видеото
    }

    // Метод, който се извиква, когато видеото приключи
    void EndCutscene(VideoPlayer vp)
    {

        // Деактивира канваса, който показва видеото
        if (videoCanvas != null)
        {
            Debug.Log("Скриване на видео Canvas.");
            videoCanvas.SetActive(false);
        }

        // Активира канваса, който показва енциклопедията след края на видеото
        if (bookCanvas != null)
        {
            Debug.Log("Показване на Canvas на енциклопедията.");
            bookCanvas.SetActive(true);

            // Добавяне на слушател за бутона за затваряне, ако не е зададен в инспектора
            if (closeButton != null)
            {
                closeButton.onClick.RemoveAllListeners(); // Премахване на всички слушатели, за да се избегне дублиране
                closeButton.onClick.AddListener(CloseBookCanvas);
            }
        }

        // Отключва курсора и прави го видим
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Рестартира VideoPlayer
        videoPlayer.Stop();
        videoPlayer.frame = 0;

        // Възстановява времето в играта
        Time.timeScale = 1;

        // Премахва слушателя, за да се избегнат потенциални изтичания на памет
        videoPlayer.loopPointReached -= EndCutscene;
    }

    // Метод за затваряне на канваса с енциклопедията
    public void CloseBookCanvas()
    {
        if (bookCanvas != null)
        {
            Debug.Log("Затваряне на Canvas на енциклопедията.");
            bookCanvas.SetActive(false);
        }

        // Заключва курсора и прави го невидим
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}