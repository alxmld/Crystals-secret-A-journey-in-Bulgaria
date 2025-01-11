using System.Collections;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public Dialogue dialogueScript; // Reference to the Dialogue script
    public GameObject triggerButton; // Reference to the button object in the scene
    public float delayBeforeDialogue = 5f; // Delay duration in seconds
    private bool isDialogueTriggered = false; // Prevent duplicate dialogue triggers

    void Start()
    {
        if (triggerButton != null)
        {
            triggerButton.SetActive(true); // Ensure the button is visible initially
        }

        if (dialogueScript == null)
        {
            Debug.LogError("Dialogue script is not assigned!");
        }
    }

    // Function to handle button press and dialogue trigger
    public void OnButtonPress()
    {
        if (!isDialogueTriggered && dialogueScript != null)
        {
            isDialogueTriggered = true;

            if (triggerButton != null)
            {
                triggerButton.SetActive(false); // Disable the button after it's pressed
            }

            StartCoroutine(DelayedDialogueStart()); // Wait before showing dialogue
        }
    }

    // Delays the start of the dialogue by 'delayBeforeDialogue' seconds
    private IEnumerator DelayedDialogueStart()
    {
        yield return new WaitForSeconds(delayBeforeDialogue); // Wait for the specified time
        dialogueScript.gameObject.SetActive(true); // Activate the dialogue box object
        dialogueScript.StartDialogue(); // Start the dialogue sequence
    }
}
