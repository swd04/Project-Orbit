using UnityEngine;

/// <summary>
/// メニュー切り替えボタンクラス
/// </summary>
public class MenuButton : MonoBehaviour
{
    [Header("メニュー管理")]
    [SerializeField] private MenuManager menuManager = null;

    [Header("開く画面")]
    [SerializeField] private MenuType menuType = MenuType.Status;

    /// <summary>
    /// ボタンが押された時の処理
    /// </summary>
    public void OnClick()
    {
        menuManager.Open(menuType);
    }
}