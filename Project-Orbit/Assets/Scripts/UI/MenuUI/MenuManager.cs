using UnityEngine;

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
    /// 現在表示している画面
    /// </summary>
    private MenuTab currentTab = MenuTab.Status;

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
        if (menuPanels.Length != System.Enum.GetValues(typeof(MenuTab)).Length)
        {
            Debug.LogError("menuPanelsの数とMenuTabの数が一致していません");
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

            TimeAttackManager.Instance.isPlayingGame = !TimeAttackManager.Instance.isPlayingGame;
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
            Open(MenuTab.Status);
        }
    }

    /// <summary>
    /// 指定した画面を表示する処理
    /// </summary>
    public void Open(MenuTab tab)
    {
        //指定された番号が存在するか確認
        if ((int)tab < 0 || (int)tab >= menuPanels.Length)
        {
            Debug.LogError("指定されたメニュー画面が存在しません");
            return;
        }

        //全ての画面を非表示にする
        foreach (GameObject panel in menuPanels)
        {
            panel.SetActive(false);
        }

        //現在の画面を更新
        currentTab = tab;

        //指定された画面のみ表示
        menuPanels[(int)currentTab].SetActive(true);
    }

    /// <summary>
    /// 次の画面へ切り替える処理
    /// </summary>
    public void NextTab()
    {
        int nextIndex = ((int)currentTab + 1) % menuPanels.Length;
        Open((MenuTab)nextIndex);
    }

    /// <summary>
    /// 前の画面へ切り替える処理
    /// </summary>
    public void PreviousTab()
    {
        int previousIndex = ((int)currentTab - 1 + menuPanels.Length) % menuPanels.Length;
        Open((MenuTab)previousIndex);
    }
}