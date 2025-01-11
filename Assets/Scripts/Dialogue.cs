using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // Dialogue box UI
    public TextMeshProUGUI textComponent; // Text field for displaying dialogue
    public string[] lines; // Array of dialogue lines
    public float textSpeed; // Typing speed for the dialogue

    private int index; // Index of the current line
    private bool isTyping = false; // Flag to check if typing is ongoing
    private Coroutine typingCoroutine; // Reference to the active typing coroutine

    // Start is called before the first frame update
    public void Start()
    {
        if (dialoguePanel == null || textComponent == null)
        {
            Debug.LogError("Dialogue Panel or Text Component is not assigned!");
            return;
        }

        textComponent.text = string.Empty; // Clear any text from previous dialogue
        dialoguePanel.SetActive(false); // Dialogue box starts hidden
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeSelf)
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine); // Stop typing
                textComponent.text = lines[index]; // Display full current line
                isTyping = false;
            }
            else if (textComponent.text == lines[index])
            {
                NextLine();
            }
        }
    }

    // Starts the dialogue
    public void StartDialogue()
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("No dialogue lines assigned!");
            return;
        }

        index = 0; // Start from the first line
        textComponent.text = string.Empty; // Clear text before starting
        dialoguePanel.SetActive(true); // Make sure the dialogue box appears
        typingCoroutine = StartCoroutine(TypeLine()); // Begin typing the first line
    }

    // Coroutine that types out a single line of dialogue
    private IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c; // Add each character to the text field
            yield return new WaitForSeconds(textSpeed); // Wait for the next character
        }
        isTyping = false; // Typing finished
    }

    // Goes to the next line or ends the dialogue
    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // Move to the next line
            textComponent.text = string.Empty; // Clear the text box
            typingCoroutine = StartCoroutine(TypeLine()); // Type out the next line
        }
        else
        {
            EndDialogue(); // End the dialogue
        }
    }

    // Ends the dialogue and hides the dialogue box
    private void EndDialogue()
    {
        textComponent.text = string.Empty; // Clear any remaining text
        dialoguePanel.SetActive(false); // Hide the dialogue box
    }
}
