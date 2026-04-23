using UnityEngine;

/// <summary>
/// スキル1つ分のUIを管理するクラス
/// </summary>
public class SkillItemUI : MonoBehaviour
{
    //このUIが対応するスキルデータ
    [SerializeField] private SkillDate skill = null;

    //親UI（スキル選択全体を管理）
    [SerializeField] private SkillSelectUI parent = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize(SkillDate skill, SkillSelectUI parent)
    {
        this.skill = skill;
        this.parent = parent;
    }

    /// <summary>
    /// ボタンがクリックされたときの処理
    /// </summary>
    public void OnClick()
    {
        //親が存在する場合のみ通知
        if (parent != null)
        {
            parent.OnSkillClicked(skill);
        }
    }
}