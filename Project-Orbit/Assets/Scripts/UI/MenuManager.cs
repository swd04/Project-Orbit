using UnityEngine;

/// <summary>
/// メニュー画面全体を管理するクラス
/// </summary>
public class MenuManager : MonoBehaviour
{
    [Header("メニュー画面")]
    [SerializeField] private GameObject menuPanel = null;

    [Header("タブパネル")]
    //プレイヤーステータスの画面パネル
    [SerializeField] private GameObject statusPanel = null;

    //装備しているスキルの画面パネル
    [SerializeField] private GameObject skillPanel = null;

    //装備しているパッシブスキルの画面パネル
    [SerializeField] private GameObject passivePanel = null;

    //セーブの画面パネル
    [SerializeField] private GameObject savePanel = null;

    //設定の画面パネル
    [SerializeField] private GameObject settingPanel = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //メニュー画面が設定されている場合は非表示にする
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        //タブパネルをすべて閉じた状態で開始
        CloseAllTabs();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //ESCキーが押されたらメニューの表示状態を切り替える
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    /// <summary>
    /// メニューの表示状態を切り替える処理
    /// </summary>
    public void ToggleMenu()
    {
        //メニュー画面が未設定の場合は処理しない
        if (menuPanel == null) return;

        //現在の表示状態を取得
        bool isActive = menuPanel.activeSelf;

        //表示状態を反転させる
        menuPanel.SetActive(!isActive);

        //メニューを開いたときは必ずステータス画面を表示
        if (!isActive)
        {
            OpenStatus();
        }
    }

    /// <summary>
    /// ステータス画面を表示
    /// </summary>
    public void OpenStatus()
    {
        //ステータスパネルのみを表示
        ShowOnly(statusPanel);
    }

    /// <summary>
    /// スキル画面を表示
    /// </summary>
    public void OpenSkill()
    {
        //スキルパネルのみを表示
        ShowOnly(skillPanel);
    }

    /// <summary>
    /// パッシブスキル画面を表示
    /// </summary>
    public void OpenPassive()
    {
        //パッシブスキルパネルのみを表示
        ShowOnly(passivePanel);
    }

    /// <summary>
    /// セーブ画面を表示
    /// </summary>
    public void OpenSave()
    {
        //セーブ画面パネルのみを表示
        ShowOnly(savePanel);
    }

    /// <summary>
    /// 設定画面を表示
    /// </summary>
    public void OpenSetting()
    {
        //設定画面パネルのみを表示
        ShowOnly(settingPanel);
    }

    /// <summary>
    /// 指定されたパネルのみを表示し、
    /// それ以外のタブパネルをすべて非表示にする処理
    /// </summary>
    private void ShowOnly(GameObject target)
    {
        //すべてのタブパネルを一旦非表示にする
        CloseAllTabs();

        //指定されたパネルが存在する場合のみ表示
        if (target != null)
        {
            target.SetActive(true);
        }
    }

    /// <summary>
    /// すべてのタブパネルを非表示にする処理
    /// </summary>
    private void CloseAllTabs()
    {
        //各タブパネルが設定されている場合のみ非表示にする
        if (statusPanel != null) statusPanel.SetActive(false);
        if (skillPanel != null) skillPanel.SetActive(false);
        if (passivePanel != null) passivePanel.SetActive(false);
        if (savePanel != null) savePanel.SetActive(false);
        if (settingPanel != null) settingPanel.SetActive(false);
    }
}