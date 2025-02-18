using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // Панел за текста
    public TextMeshProUGUI textComponent; // Текстовото поле
    public string[] lines; // Масив от редове тексто за показване 
    public float textSpeed; // Бързина на показване на текста
    private int index; 
    private bool isTyping = false; // Задава, че в момента текстът не се изписва
    private Coroutine typingCoroutine; 

    public void Start()
    {
        if (dialoguePanel == null || textComponent == null)
        {
            Debug.LogError("Dialogue Panel or Text Component is not assigned!");
            return;
        }

        textComponent.text = string.Empty; // Маха предишен текст
        dialoguePanel.SetActive(false); // Скрива панела за текст
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeSelf) // Ако левия бутон на мишката е натиснат докато панел е активен
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine); // Спира писането
                textComponent.text = lines[index]; // И показва целия текущ ред с текст
                isTyping = false; // Задава, че в момента текстът не се изписва
            }
            else if (textComponent.text == lines[index]) // Ако нищо не е натиснато
            {
                NextLine(); // Продължава със следващия ред с текст
            }
        }
    }

    public void StartDialogue()
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("No dialogue lines assigned!");
            return;
        }

        index = 0; // Започва с първия ред текст
        textComponent.text = string.Empty; // Премахва текст преди да започне
        dialoguePanel.SetActive(true); // Активира панела за текста
        typingCoroutine = StartCoroutine(TypeLine()); //
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c; // За добавяне на символи в текстовото поле
            yield return new WaitForSeconds(textSpeed); // Изчаква малко преди да изпише следващия символ 
        }
        isTyping = false; // Задава, че в момента текстът не се изписва
    }

    private void NextLine()
    {
        if (index < lines.Length - 1) // Ако текущият ред с текст е изписан напълно
        {
            index++; // Отива на следващия ред текст
            textComponent.text = string.Empty; // Премахва предишния текст
            typingCoroutine = StartCoroutine(TypeLine()); // Изписва следващия ред с текст
        }
        else
        {
            EndDialogue();
        }
    }

    // Функция за приклюване и скриване на прозореца за текст и текстовото поле
    private void EndDialogue()
    {
        textComponent.text = string.Empty; // Премахва останал текст
        dialoguePanel.SetActive(false); // Скрива прозореца с текст
    }
}
