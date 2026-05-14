using UnityEngine;

/// <summary>
/// カーソル表示固定用
/// </summary>
public class CursorVisibleFix : MonoBehaviour
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }
}