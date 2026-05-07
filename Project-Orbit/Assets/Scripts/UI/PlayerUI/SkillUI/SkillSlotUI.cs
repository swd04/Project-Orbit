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
    /// スキルデータを受け取り、UI表示を更新する処理
    /// </summary>
    public void Set(SkillDate skill, bool isSelected)
    {
        //スキルアイコンを設定
        icon.sprite = skill.icon;

        //スキル名テキストを設定
        skillName.text = skill.skillName;

        //選択中なら不透明、非選択なら半透明にする
        canvasGroup.alpha = isSelected ? 1.0f : 0.5f;

        //選択中のスキルを少し拡大して強調表示
        transform.localScale = isSelected ? Vector3.one * 2f : Vector3.one;
    }
}