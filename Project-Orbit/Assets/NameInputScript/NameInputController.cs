using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// 名前入力処理を管理するクラス
/// </summary>
public class NameInputController : SingletonBehaviour<NameInputController>
{
    [Header("入力した名前の取得")]
    [SerializeField] private TMP_InputField nameInputField = null;

    [Header("名前の長さ制限")]
    [SerializeField] private int maxNameLength = 0;

    [Header("UserDataHolder")]
    [SerializeField] private UserDataHolder userDataHolder = null;


    private void Start()
    {
        // 入力フィールドの初期化
        nameInputField.text = "";

        nameInputField.characterLimit = maxNameLength;

        // 入力開始
        nameInputField.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GetPlayerName() != null)
            {
                SceneManager.LoadScene("TimeAttackMainScene");
            }
        }
    }

    /// <summary>
    /// 入力した名前を取得するメソッド
    /// </summary>
    public string GetPlayerName()
    {
        if (string.IsNullOrWhiteSpace(nameInputField.text))
        {
            // 名前の入力がない場合UIで名前の入力を促す

            Debug.Log("名前を入力して");

            return null;
        }
        else
        {
            Debug.Log("入力された名前: " + nameInputField.text);

            // UserDataHolderへ保存
            userDataHolder.userName = nameInputField.text;

            return userDataHolder.userName;
        }
    }
}