using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// 名前入力時の案内メッセージを管理するクラス
/// </summary>
public class NameInputMessageUI : MonoBehaviour
{
    [Header("メッセージ表示UI")]
    [SerializeField] private TMP_Text messageText = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //初期状態では非表示
        messageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 名前未入力時のメッセージを表示
    /// </summary>
    public void ShowEmptyMessage()
    {
        ShowMessage("名前を入力してください");
    }

    /// <summary>
    /// メッセージを表示する処理
    /// </summary>
    private void ShowMessage(string message)
    {
        //メッセージ表示UIが設定されていない場合
        if (messageText == null)
        {
            return;
        }

        //メッセージを設定
        messageText.text = message;

        //メッセージを表示
        messageText.gameObject.SetActive(true);
    }
}