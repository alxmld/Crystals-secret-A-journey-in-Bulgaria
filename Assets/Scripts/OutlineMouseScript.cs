using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]     // Забранява на скрипта да се добавя повече от веднъж като компонент към обект

// Клас, контролиращ кога огражденията около обектите да се включват
public class OutlineMouseScript : MonoBehaviour
{
    public Outline outline;

    // Когато мишката е върху обекта, очертанията се включват
    void OnMouseOver()
    {
        outline.enabled = true;     // включва очертанията
        Debug.Log("on the book");
    }

    // Когато мишката не е върху обекта, очертанията се изключват
    void OnMouseExit()
    {
        outline.enabled = false;    // изключва очертанията
    }
}
