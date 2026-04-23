using UnityEngine;

/// <summary>
/// スキル選択UI全体を管理するクラス
/// </summary>
public class SkillSelectUI : MonoBehaviour
{
    [Header("スキル管理クラス")]
    [SerializeField] private SkillActivation skillActivation = null;

    [Header("装備中スキルの親オブジェクト")]
    [SerializeField] private Transform equippedContent = null;

    [Header("未装備スキルの親オブジェクト")]
    [SerializeField] private Transform unequippedContent = null;

    [Header("スキルUIプレハブ")]
    [SerializeField] private GameObject skillItemPrefab = null;

    /// <summary>
    /// スキルがクリックされたときの処理
    /// </summary>
    public void OnSkillClicked(SkillDate skill)
    {
        //参照が無い場合は何もしない
        if (skillActivation == null) return;

        //装備状態によって処理を分岐
        if (skillActivation.IsEquipped(skill))
        {
            //装備中 → 解除
            skillActivation.UnequipSkill(skill);
        }
        else
        {
            //未装備 → 装備
            skillActivation.EquipSkill(skill);
        }

        //UIを更新
        RefreshUI();
    }

    /// <summary>
    /// UIの再構築処理
    /// </summary>
    public void RefreshUI()
    {
        foreach (Transform child in equippedContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in unequippedContent)
        {
            Destroy(child.gameObject);
        }

        //装備中スキルの表示
        foreach (var skill in skillActivation.GetEquippedSkills())
        {
            var obj = Instantiate(skillItemPrefab, equippedContent);
            obj.GetComponent<SkillItemUI>().Initialize(skill, this);
        }

        //未装備スキルの表示
        foreach (var skill in skillActivation.GetUnequippedSkills())
        {
            var obj = Instantiate(skillItemPrefab, unequippedContent);
            obj.GetComponent<SkillItemUI>().Initialize(skill, this);
        }
    }
}