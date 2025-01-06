using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool isTyping = false; // Flag to check if typing is ongoing

    // Start is called before the first frame update
    public void Start()
    {
        textComponent.text = string.Empty; // Clear any text from previous dialogue
        gameObject.SetActive(false); // Dialogue box starts hidden
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping) // Stop typing and show full line instantly
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
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
        index = 0; // Start from the first line
        textComponent.text = string.Empty; // Clear text before starting
        gameObject.SetActive(true); // Make sure the dialogue box appears after button press
        StartCoroutine(TypeLine()); // Begin typing out the first line
    }

    // Coroutine that types out a single line of dialogue
    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c; // Add each character to the screen
            yield return new WaitForSeconds(textSpeed); // Wait for the next character
        }
        isTyping = false;
    }

    // Goes to the next line or ends the dialogue
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // Move to the next line
            textComponent.text = string.Empty; // Clear the text box
            StartCoroutine(TypeLine()); // Type out the next line
        }
        else
        {
            EndDialogue(); // End the dialogue and hide the box
        }
    }

    // Ends the dialogue and hides the dialogue box
    void EndDialogue()
    {
        textComponent.text = string.Empty; // Clear any remaining text
        gameObject.SetActive(false); // Hide the dialogue box
    }
}
