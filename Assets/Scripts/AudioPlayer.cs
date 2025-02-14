using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] GameObject activeCave;
    public AudioSource bgMusicSource;
    private bool activeCaveIsActive = false;
    private bool isPlaying = false;

    // Update is called once per frame
    void Update()
    {
        if (activeCave.activeSelf)
        {
            activeCaveIsActive = true;
        } 
        else
        {
            activeCaveIsActive = false;
        }

        if (activeCaveIsActive && !isPlaying)
        {
            bgMusicSource.Play();
            isPlaying = true;
        }
        else if (!activeCaveIsActive && isPlaying)
        {
            bgMusicSource.Stop();
            activeCaveIsActive = false;
            isPlaying = false;
        }
    }
}
