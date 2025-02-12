using UnityEngine;

public class MapsLockCursor : MonoBehaviour
{
    // Този метод се вика от OnClick евент на бутон
    public void LockAndHideCursor()
    {
        // Заключва и скрива курсора в центъра на екрана
        Cursor.lockState = CursorLockMode.Locked;

        // Скрива курсора
        Cursor.visible = false;
    }
}
