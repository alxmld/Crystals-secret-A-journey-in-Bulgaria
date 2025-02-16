using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private GameObject activeGameObject;
    public AudioSource bgMusicSource;
    public float fadeDuration = 1.5f; // Time in seconds for fade effect
    private bool isActive = false;
    private Coroutine fadeCoroutine;

    void Update()
    {
        bool shouldBeActive = activeGameObject.activeSelf;

        if (shouldBeActive && !isActive)
        {
            isActive = true;
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeAudio(bgMusicSource, 1f, fadeDuration)); // Fade in
        }
        else if (!shouldBeActive && isActive)
        {
            isActive = false;
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeAudio(bgMusicSource, 0f, fadeDuration)); // Fade out
        }
    }

    private IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        if (targetVolume > 0 && !audioSource.isPlaying) 
            audioSource.Play(); // Start playing if fading in

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;

        if (targetVolume == 0) 
            audioSource.Stop(); // Stop playing if completely faded out
    }
}
