using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text theText; // Референция към текстов компонент, който ще променяме

    public Color normalColor = Color.white; // Цвят на текста в нормално състояние
    public Color highlightedColor = Color.red; // Цвят на текста, когато мишката е върху бутона
    public Color pressedColor = Color.yellow; // Цвят на текста, когато бутонът е натиснат
    public Color disabledColor = Color.gray; // Цвят на текста, когато бутонът е деактивиран

    private Button button; // За бутона

    private void Start()
    {
        // Взима компонента Button и проверяваме дали е наличен
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Няма намерен Button компонент на този GameObject.");
        }

        // Задава началния цвят на текста
        if (theText != null)
        {
            UpdateTextColor();
        }
    }

    // Метод, който се извиква, когато мишката е върху бутона
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable) // Проверява дали бутонът е интерактивен
        {
            theText.color = highlightedColor; // Променя цвета на текста на highlightedColor
        }
    }

    // Метод, който се извиква, когато мишката напусне бутона
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            theText.color = normalColor; // Връща цвета на текста към нормалния цвят
        }
    }

    // Метод, който се извиква, когато бутонът бъде натиснат
    public void OnPointerDown(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            theText.color = pressedColor; // Променя цвета на текста на pressedColor
        }
    }

    // Метод, който се извиква, когато бутонът е освободен
    public void OnPointerUp(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            theText.color = normalColor; // Връща цвета на текста към нормалния цвят
        }
    }

    private void Update()
    {
        if (button != null && theText != null)
        {
            if (!button.interactable && theText.color != disabledColor)
            {
                theText.color = disabledColor; // Ако бутонът е деактивиран, задава цвета на disabledColor
            }
            else if (button.interactable && theText.color == disabledColor)
            {
                theText.color = normalColor; // Ако бутонът е активиран, връща цвета към нормалния
            }
        }
    }

    // Метод за обновяване на цвета на текста според състоянието на бутона
    private void UpdateTextColor()
    {
        if (button != null && theText != null)
        {
            theText.color = button.interactable ? normalColor : disabledColor; // Задава цвета в зависимост от това дали бутонът е интерактивен
        }
    }
}