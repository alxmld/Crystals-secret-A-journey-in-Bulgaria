using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject[] hoverSprites; // Array of hover sprites
    private Button button; // Reference to the button component

    private void Start()
    {
        // Get the Button component on this GameObject
        button = GetComponent<Button>();

        // Hide all sprites initially
        foreach (GameObject sprite in hoverSprites)
        {
            if (sprite != null)
                sprite.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show sprites only if the button is interactable
        if (button != null && button.interactable)
        {
            foreach (GameObject sprite in hoverSprites)
            {
                if (sprite != null)
                    sprite.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide all sprites when not hovering
        foreach (GameObject sprite in hoverSprites)
        {
            if (sprite != null)
                sprite.SetActive(false);
        }
    }
}
