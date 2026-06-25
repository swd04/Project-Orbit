using UnityEngine;
using TMPro;

/// <summary>
/// コア一覧1行分の表示UI
/// </summary>
public class CoreItemUI : MonoBehaviour
{
    [Header("コア名")]
    [SerializeField] private TMP_Text coreNameText = null;

    [Header("所持数")]
    [SerializeField] private TMP_Text countText = null;

    /// <summary>
    /// コア情報表示処理
    /// </summary>
    public void SetData(string name, int count)
    {
        //コア名表示
        coreNameText.text = name;

        //所持数表示
        countText.text = $"×{count}";
    }
}