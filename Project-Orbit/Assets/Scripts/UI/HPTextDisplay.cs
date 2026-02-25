using UnityEngine;
using TMPro;

/// <summary>
/// HPの数字表示を管理するクラス
/// </summary>
public class HPTextDisplay : MonoBehaviour
{
    [Header("表示用テキスト")]
    //HPを数字で表示するTextMeshPro
    [SerializeField] private TMP_Text hpText = null;

    /// <summary>
    /// HP表示を更新する処理
    /// </summary>
    public void UpdateHP(float current, float max)
    {
        //Textが設定されていなければ何もしない
        if (hpText == null) return;

        //現在HP/最大HPの形式で表示
        hpText.text = $"{Mathf.CeilToInt(current)}/{Mathf.CeilToInt(max)}";
    }
}