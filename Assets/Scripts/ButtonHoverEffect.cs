using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject[] hoverSprites;     // Масив от обекти, които ще се показват при ховър
    private Button button; // За бутона, на който е закачен скрипта

    private void Start()
    {
        // Взима компонента Button
        button = GetComponent<Button>();

        // Скрива всички спрайтове в началото на играта
        foreach (GameObject sprite in hoverSprites)
        {
            if (sprite != null)
                sprite.SetActive(false);
        }
    }

    // Метод, който се изпълнява, когато курсорът навлезе върху бутона
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable) // Ако бутонът е интерактивен
        {
            foreach (GameObject sprite in hoverSprites)
            {
                if (sprite != null)
                    sprite.SetActive(true); // Показва всички ховър спрайтове
            }
        }
    }

    // Метод, който се изпълнява, когато курсорът излезе от бутона
    public void OnPointerExit(PointerEventData eventData)
    {
        // Скрива всички ховър спрайтове
        foreach (GameObject sprite in hoverSprites)
        {
            if (sprite != null)
                sprite.SetActive(false);
        }
    }
}
