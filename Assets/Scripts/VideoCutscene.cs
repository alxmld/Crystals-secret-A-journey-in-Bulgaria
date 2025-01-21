using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoCutscene : MonoBehaviour, IInteractable
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public GameObject videoCanvas; // Reference to the canvas displaying the video
    public GameObject bookCanvas;
    public Button closeButton; // Reference to the Button component for closing

    public void Interact()
    {
        PlayCutscene();
    }

    private void PlayCutscene()
    {
        if (videoPlayer == null || videoCanvas == null)
        {
            Debug.LogWarning("Missing VideoPlayer or VideoCanvas reference!");
            return;
        }

        videoPlayer.Stop();
        videoPlayer.frame = 0;

        // Clear the render texture to avoid showing the last frame
        if (videoPlayer.targetTexture != null)
        {
            RenderTexture renderTexture = videoPlayer.targetTexture;
            RenderTexture.active = renderTexture;
            GL.Clear(true, true, Color.black); // Clears the texture with a black background
            RenderTexture.active = null;
        }

        videoCanvas.SetActive(true); // Activate the canvas to show the video
        Debug.Log("Attempting to play video.");

        Time.timeScale = 0;

        // Ensure the listener is only added once to avoid stacking
        videoPlayer.loopPointReached -= EndCutscene; // Unsubscribe first to avoid duplication
        videoPlayer.loopPointReached += EndCutscene;

        videoPlayer.Play();
    }

    private void EndCutscene(VideoPlayer vp)
    {
        Debug.Log("Cutscene Ended");

        // Deactivate the canvas showing the video
        if (videoCanvas != null)
        {
            Debug.Log("Hiding video canvas.");
            videoCanvas.SetActive(false);
        }

        // Activate the bookCanvas to display it after the cutscene ends
        if (bookCanvas != null)
        {
            Debug.Log("Showing book canvas.");
            bookCanvas.SetActive(true);

            // Optionally assign the close button click event if not set in the inspector
            if (closeButton != null)
            {
                closeButton.onClick.RemoveAllListeners(); // Avoid stacking listeners
                closeButton.onClick.AddListener(CloseBookCanvas);
            }
        }

        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Reset the VideoPlayer
        videoPlayer.Stop();
        videoPlayer.frame = 0;

        // Resume game time
        Time.timeScale = 1;

        // Clean up the listener to avoid potential memory leaks
        videoPlayer.loopPointReached -= EndCutscene;
    }

    public void CloseBookCanvas()
    {
        if (bookCanvas != null)
        {
            Debug.Log("Closing book canvas.");
            bookCanvas.SetActive(false);
        }

        // Lock the cursor and make it invisible (if necessary)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
