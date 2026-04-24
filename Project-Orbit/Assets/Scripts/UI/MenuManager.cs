using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// メニュー画面全体の表示制御とタブ切り替えを管理するクラス
/// </summary>
public class MenuManager : MonoBehaviour
{
    [Header("メニュー画面")]
    //メニュー全体の表示・非表示を切り替える
    [SerializeField] private GameObject menuPanel = null;

    [Header("メニュー画面のタブ一覧")]
    //スキル・設定などの各タブをまとめたリスト
    [SerializeField] private List<GameObject> menuTabPanels = new List<GameObject>();

    //現在表示中のタブ状態
    private MenuTab currentTab;

    /// <summary>
    /// メニューが開いているかどうか
    /// 他クラスから参照するためのプロパティ
    /// </summary>
    public bool IsMenuOpen => menuPanel != null && menuPanel.activeSelf;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //メニューが設定されていれば初期状態では非表示にする
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        //すべてのタブを非表示にして開始
        CloseAllTabs();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //ESCキーでメニューの開閉を切り替える
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    /// <summary>
    /// メニューの表示/非表示を切り替える処理
    /// </summary>
    public void ToggleMenu()
    {
        //メニューが未設定なら何もしない
        if (menuPanel == null) return;

        //現在の表示状態を取得
        bool isActive = menuPanel.activeSelf;

        //表示状態を反転
        menuPanel.SetActive(!isActive);

        //メニューを開いたときは最初のタブを表示
        if (!isActive)
        {
            OpenTab(MenuTab.Status);
        }
    }

    /// <summary>
    /// 指定したタブのみを表示する処理
    /// </summary>
    public void OpenTab(MenuTab tab)
    {
        //一度すべてのタブを閉じる
        CloseAllTabs();

        //enum → indexに変換
        int index = (int)tab;

        //範囲チェック
        if (index < 0 || index >= menuTabPanels.Count)
        {
            Debug.LogWarning("Tab index out of range: " + tab);
            return;
        }

        //対応するタブのみ表示
        menuTabPanels[index].SetActive(true);

        //現在のタブ状態を更新
        currentTab = tab;
    }

    /// <summary>
    /// ButtonのOnClick用
    /// </summary>
    public void OpenTabByIndex(int index)
    {
        OpenTab((MenuTab)index);
    }

    /// <summary>
    /// すべてのタブを非表示にする処理
    /// </summary>
    private void CloseAllTabs()
    {
        //登録されているすべてのタブを順番に非表示
        foreach (var tab in menuTabPanels)
        {
            if (tab != null)
            {
                tab.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Inspector変更時のチェック処理
    /// </summary>
    private void OnValidate()
    {
        if (menuTabPanels.Count != System.Enum.GetValues(typeof(MenuTab)).Length)
        {
            Debug.LogWarning("MenuTabとListの数が一致してない！");
        }
    }
}