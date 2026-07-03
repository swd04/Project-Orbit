using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// メニュー表示管理クラス
/// </summary>
public class MenuManager : MonoBehaviour
{
    [Header("メニュー画面")]
    [SerializeField] private GameObject menuPanel = null;

    /// <summary>
    /// メニューが開いているか
    /// </summary>
    public bool IsMenuOpen => menuPanel.activeSelf;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //メニューを非表示にする
        menuPanel.SetActive(false);

        //カーソルを非表示
        Cursor.visible = false;

        //カーソルを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //ESCキーでメニューを開閉
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    /// <summary>
    /// メニュー開閉処理
    /// </summary>
    private void ToggleMenu()
    {
        //現在の状態を反転
        bool isOpen = !menuPanel.activeSelf;

        //メニュー表示切り替え
        menuPanel.SetActive(isOpen);

        //メニュー表示中はカーソルを表示
        Cursor.visible = isOpen;

        //メニュー表示中はカーソル固定解除
        //メニュー非表示時はカーソルを中央固定
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}