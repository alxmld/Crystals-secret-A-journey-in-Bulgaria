using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource bgMusicSource;  // Background music source
    public AudioSource uiAudioSource;  // UI sound source

    [Header("Audio Clips")]
    public AudioClip defaultMusic;  // Default background music
    public AudioClip hoverSound;    // UI hover sound
    public AudioClip[] objectMusic; // Array for different GameObject music

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayHoverSound()
    {
        if (uiAudioSource != null && hoverSound != null)
        {
            uiAudioSource.PlayOneShot(hoverSound);
        }
    }

    public void PlayBackgroundMusic(AudioClip newMusic)
    {
        if (bgMusicSource.clip == newMusic) return; // Avoid restarting same track
        bgMusicSource.clip = newMusic;
        bgMusicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        bgMusicSource.Stop();
    }
}
