using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public Dialogue dialogueScript; // Reference to the Dialogue script
    public GameObject triggerButton; // Reference to the button object in the scene
    public float delayBeforeDialogue = 5f; // Delay duration in seconds

    void Start()
    {
        if (triggerButton != null)
        {
            triggerButton.SetActive(true); // Ensure the button is visible/active initially
        }
    }

    // Function to handle button press and dialogue trigger
    public void OnButtonPress()
    {
        if (dialogueScript != null)
        {
            if (triggerButton != null)
            {
                triggerButton.SetActive(false); // Disable the button after it's pressed
            }
            StartCoroutine(DelayedDialogueStart()); // Wait before showing dialogue
        }
        else
        {
            Debug.LogWarning("Dialogue script is not assigned!");
        }
    }

    // Delays the start of the dialogue by 'delayBeforeDialogue' seconds
    private IEnumerator DelayedDialogueStart()
    {
        yield return new WaitForSeconds(delayBeforeDialogue); // Wait for the specified time
        dialogueScript.gameObject.SetActive(true); // Activate the dialogue box
        dialogueScript.StartDialogue(); // Start the dialogue after showing the box
    }
}
