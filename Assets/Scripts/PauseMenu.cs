using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Assign your pause menu GameObject here
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            isPaused = !isPaused;

            if (isPaused)
                ActivateMenu();
            else
                DeactivateMenu();
        }
    }

    private void ActivateMenu()
    {
        Debug.Log("Activating Pause Menu");
        Time.timeScale = 0; // Stop the game
        AudioListener.pause = true; // Pause audio
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        CursorManager.SetCursorLockState(false); // Unlock and show the cursor
    }

    public void DeactivateMenu()
    {
        Debug.Log("Deactivating Pause Menu");
        Time.timeScale = 1; // Resume the game
        AudioListener.pause = false; // Resume audio
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        isPaused = false;
        CursorManager.SetCursorLockState(true); // Lock and hide the cursor
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
