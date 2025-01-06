using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text theText;

    public Color normalColor = Color.white;
    public Color highlightedColor = Color.red;
    public Color pressedColor = Color.yellow;
    public Color disabledColor = Color.gray;

    private Button button;

    private void Start()
    {
        // Get the button component and ensure it's working
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("No Button component found on this GameObject.");
        }

        // Set the initial text color
        if (theText != null)
        {
            UpdateTextColor();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            theText.color = highlightedColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            theText.color = normalColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            theText.color = pressedColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            theText.color = normalColor; // Revert to normal color after press
        }
    }

    private void Update()
    {
        // Dynamically handle disabled state
        if (button != null && theText != null)
        {
            if (!button.interactable && theText.color != disabledColor)
            {
                theText.color = disabledColor;
            }
            else if (button.interactable && theText.color == disabledColor)
            {
                theText.color = normalColor;
            }
        }
    }

    private void UpdateTextColor()
    {
        if (button != null && theText != null)
        {
            theText.color = button.interactable ? normalColor : disabledColor;
        }
    }
}
