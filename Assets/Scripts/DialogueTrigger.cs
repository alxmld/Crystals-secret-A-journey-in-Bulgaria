using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public Dialogue dialogueScript; // Reference to the Dialogue script
    public Button[] triggerButtons; // Array of buttons in the scene
    public float delayBeforeDialogue = 5f; // Delay duration in seconds
    private bool isDialogueTriggered = false; // Prevent duplicate dialogue triggers

    void Start()
    {
        if (dialogueScript == null)
        {
            Debug.LogError("Dialogue script is not assigned!");
            return;
        }

        if (triggerButtons.Length == 0)
        {
            Debug.LogError("No trigger buttons assigned!");
            return;
        }

        // Assign the OnButtonPress method to each button's onClick event
        foreach (Button button in triggerButtons)
        {
            button.gameObject.SetActive(true); // Ensure buttons are visible
            button.onClick.AddListener(() => OnButtonPress());
        }
    }

    // Function to handle button press and trigger dialogue
    public void OnButtonPress()
    {
        if (!isDialogueTriggered)
        {
            isDialogueTriggered = true;
            StartCoroutine(DelayedDialogueStart());
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
