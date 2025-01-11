using UnityEngine;

public static class CursorManager
{
    public static void SetCursorLockState(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
}
