using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スキルホイール上の「1スロット分」のUI表示を担当するクラス
/// </summary>
public class SkillSlotUI : MonoBehaviour
{
    [Header("スキルアイコン表示用Image")]
    [SerializeField] private Image icon = null;

    [Header("スキル名表示用Text")]
    [SerializeField] private TextMeshProUGUI skillName = null;

    [Header("透明度制御用")]
    [SerializeField] private CanvasGroup canvasGroup = null;

    /// <summary>
    /// スキル情報をUIに反映する処理
    /// </summary>
    public void Set(SkillDate skill, bool isSelected)
    {
        //スキルデータからアイコンと名前を設定
        icon.sprite = skill.icon;
        skillName.text = skill.skillName;

        canvasGroup.alpha = isSelected ? 1.0f : 0.5f;
        transform.localScale = isSelected ? Vector3.one * 1.2f : Vector3.one;
    }
}