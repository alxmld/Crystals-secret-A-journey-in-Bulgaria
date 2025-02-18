using UnityEngine;

public static class CursorManager // Статичен клас за управление на курсора
{
    public static void SetCursorLockState(bool isLocked) // Метод за задаване на състоянието на курсора
    {
        // Ако isLocked е true, заключваме курсора, в противен случай го освобождаваме
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        
        // Ако курсорът е заключен, той става невидим, в противен случай остава видим
        Cursor.visible = !isLocked;
    }
}
