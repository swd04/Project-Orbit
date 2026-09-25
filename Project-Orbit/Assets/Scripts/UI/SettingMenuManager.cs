using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 設定画面を表示管理クラス
/// </summary>
public class SettingMenuManager : MonoBehaviour
{
    [Header("設定画面")]
    [SerializeField] private Image settingMenuImage = null;

    [Header("非表示にするUI")]
    [SerializeField] private GameObject[] hiddenUI = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //起動時は設定画面を非表示にする
        settingMenuImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 設定画面を開く処理
    /// </summary>
    public void OpenSettingMenu()
    {
        //設定画面を表示する
        settingMenuImage.gameObject.SetActive(true);

        //設定画面を開いている間、指定したUIを非表示にする
        foreach (GameObject ui in hiddenUI)
        {
            //UIが設定されている場合
            if (ui != null)
            {
                //UIを非表示にする
                ui.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 設定画面を閉じる処理
    /// </summary>
    public void CloseSettingMenu()
    {
        //設定画面を非表示にする
        settingMenuImage?.gameObject.SetActive(false);

        //設定画面を閉じたら、非表示にしていたUIを表示する
        foreach (GameObject ui in hiddenUI)
        {
            //UIが設定されている場合
            if (ui != null)
            {
                //UIを表示する
                ui.SetActive(true);
            }
        }
    }
}