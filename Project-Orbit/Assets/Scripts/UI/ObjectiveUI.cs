using TMPro;
using UnityEngine;

/// <summary>
/// 目標表示UI
/// </summary>
public class ObjectiveUI : MonoBehaviour
{
    [Header("目標テキスト")]
    [SerializeField] private TMP_Text objectiveText = null;

    /// <summary>
    /// 目標更新
    /// </summary>
    public void SetObjective(string objective)
    {
        objectiveText.text = objective;
    }

    /// <summary>
    /// 目標クリア
    /// </summary>
    public void ClearObjective()
    {
        objectiveText.text = "";
    }
}