using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Обектът, който определя кога музиката трябва да се пуска или спира
    [SerializeField] private GameObject activeGameObject;  
    public AudioSource bgMusicSource;  // Източник на аудио
    public float fadeDuration = 1.5f;  // За fade in/fade out ефекта
    private bool isActive = false;  
    private Coroutine fadeCoroutine; // Променлива за управление на корутината, за да не се стартират няколко едновременно

    void Update()
    {
        // Проверяваме дали активният обект е включен
        bool shouldBeActive = activeGameObject.activeSelf;

        // Ако обектът току-що е станал активен и музиката не е пусната
        if (shouldBeActive && !isActive)
        {
            isActive = true;
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine); // Спиране на предишна корутина, ако има
            fadeCoroutine = StartCoroutine(FadeAudio(bgMusicSource, 1f, fadeDuration)); // За fade in ефекта
        }
        // Ако обектът току-що е станал неактивен и музиката е пусната
        else if (!shouldBeActive && isActive)
        {
            isActive = false;
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine); // Спира предишна корутина ако има
            fadeCoroutine = StartCoroutine(FadeAudio(bgMusicSource, 0f, fadeDuration)); // За fade out ефекта
        }
    }

    private IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float duration)
    {
        // Запазва началната сила на звука
        float startVolume = audioSource.volume;
        float time = 0f;

        // Ако увеличаваме звука и музиката не е пусната, я стартираме
        if (targetVolume > 0 && !audioSource.isPlaying) 
            audioSource.Play();  

        // Постепенно променяме звука през определеното време
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        // Гарантира, че крайният обем наистина е достигнат
        audioSource.volume = targetVolume;

        // Ако звукът е напълно заглушен, спира възпроизвеждането
        if (targetVolume == 0) 
            audioSource.Stop();  
    }
}
