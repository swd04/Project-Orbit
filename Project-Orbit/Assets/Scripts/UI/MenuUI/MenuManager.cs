using UnityEngine;

/// <summary>
/// メニューのタブ
/// </summary>
public enum MenuType
{
    /// <summary>
    /// ステータス
    /// </summary>
    Status,
    /// <summary>
    /// スキル
    /// </summary>
    Skill,
    /// <summary>
    /// パッシブスキル
    /// </summary>
    Passive,
    /// <summary>
    /// 設定
    /// </summary>
    Setting
}

/// <summary>
/// メニュー表示管理クラス
/// </summary>
public class MenuManager : MonoBehaviour
{
    [Header("メニュー画面")]
    [SerializeField] private GameObject menuPanel = null;

    [Header("各画面")]
    [SerializeField] private GameObject[] menuPanels = null;

    /// <summary>
    /// メニューが開いているか
    /// </summary>
    public bool IsMenuOpen => menuPanel.activeSelf;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //登録されている画面数とMenuTypeの数が一致しているか確認
        if (menuPanels.Length != System.Enum.GetValues(typeof(MenuType)).Length)
        {
            Debug.LogError("menuPanelsの数とMenuTypeの数が一致していません");
        }

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

        //メニューを開いたらステータス画面を表示する
        if (isOpen)
        {
            Open(MenuType.Status);
        }
    }

    /// <summary>
    /// メニューを開き、指定した画面を表示する処理
    /// </summary>
    public void Open(MenuType type)
    {
        //指定された番号が存在するか確認
        if ((int)type >= menuPanels.Length)
        {
            Debug.LogError("指定されたメニュー画面が存在しません");
            return;
        }

        //全ての画面を非表示にする
        foreach (GameObject panel in menuPanels)
        {
            panel.SetActive(false);
        }

        //指定された画面のみ表示する
        menuPanels[(int)type].SetActive(true);
    }
}