using UnityEngine;
using UnityEngine.UI;

public class PauseMenuDisabler : MonoBehaviour
{
    public GameObject pauseMenu; // Assign the UI Button in the Inspector
    public GameObject otherGameObject; // Assign the GameObject that should disable the button

    void Update()
    {
        if (pauseMenu != null && otherGameObject != null)
        {
            pauseMenu.GetComponent<PauseMenu>().enabled = false;
        }
    }
}
