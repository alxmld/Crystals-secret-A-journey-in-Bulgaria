using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public GameObject book; // Assign the book object in the Inspector
    public VideoPlayer videoPlayer; // Assign a VideoPlayer component in the Inspector
    public float interactionDistance = 3.0f; // Max distance to interact with the book

    void Awake()
    {
        if (book == null)
        {
            Debug.LogError("Book GameObject is not assigned. Please assign it in the Inspector.");
        }
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is not assigned. Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        // Detect interaction key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed!");

            // Check if the player is near the book
            if (IsPlayerNearBook())
            {
                Debug.Log("Player is near the book. Playing video.");
                PlayVideo();
            }
            else
            {
                Debug.Log("Player is too far from the book to interact.");
            }
        }
    }

    private bool IsPlayerNearBook()
    {
        if (book == null)
        {
            return false;
        }

        float distance = Vector3.Distance(book.transform.position, Camera.main.transform.position);
        return distance <= interactionDistance;
    }

    private void PlayVideo()
    {
       if (videoPlayer != null)
        {
        if (!videoPlayer.isPlaying)
            {
            videoPlayer.Play();
            Debug.Log("Video is now playing.");
            }
        else
            {
            Debug.Log("Video is already playing.");
            }
        }
    else
        {
        Debug.LogError("No VideoPlayer assigned.");
        }
    }
}
