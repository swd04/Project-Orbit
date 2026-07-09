using UnityEngine;
using TMPro;

public class NameInputController : MonoBehaviour
{
    [Header("入力した名前の取得")]
    [SerializeField] private TMP_InputField nameInputField = null;

    private void Start()
    {
        // 入力フィールドの初期化
        nameInputField.text = "";
    }

    /// <summary>
    /// 入力した名前を取得するメソッド
    /// </summary>
    public string GetPlayerName()
    {
        if (nameInputField.text == "")
        {
            return "NoName";
        }

        // 入力された名前を取得
        return nameInputField.text;
    }
}
