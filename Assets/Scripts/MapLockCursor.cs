using UnityEngine;

public class MapsLockCursor : MonoBehaviour
{
    // Call this method from the UI Button's OnClick event
    public void LockAndHideCursor()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Hide the cursor
        Cursor.visible = false;

        Debug.Log("Cursor is now locked and hidden.");
    }
}
