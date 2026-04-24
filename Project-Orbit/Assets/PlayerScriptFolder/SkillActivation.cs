using System.Collections.Generic;
using UnityEngine;

public class SkillActivation : MonoBehaviour
{
    [Header("プレイヤーの装備スキルリスト")]
    [SerializeField] private List<SkillDate> equippedSkills = new List<SkillDate>();

    [Header("プレイヤーの未装備スキルリスト")]
    [SerializeField] private List<SkillDate> unequippedSkills = new List<SkillDate>();

    [Header("ホイールの回転する値を取得")]
    [SerializeField] private float mouseScrollWheelValue = 0.0f;

    [Header("現在選んでいるスキルナンバー")]
    [SerializeField] private int currentSelectSkillNumber = 0;

    [Header("スキルホイールUI")]
    [SerializeField] private SkillWheelUI skillWheelUI = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //スキルホイールUIを初期状態で表示
        skillWheelUI.Initialize(equippedSkills, 0);
    }

    private void Update()
    {
        mouseScrollWheelValue = Input.GetAxis("Mouse ScrollWheel");


        SelectSkill();
        ActivationSkill();
    }

    private void SelectSkill()
    {
        if (mouseScrollWheelValue > 0)
        {
            //ホイールを上に動かした場合
            //currentSelectSkillNumber = (currentSelectSkillNumber + 1) % playerSkillList.Count;

            //UIに選択変更を通知
            skillWheelUI.RotateCounterClockwise();
        }
        else if (mouseScrollWheelValue < 0)
        {
            //ホイールを下に動かした場合
            //currentSelectSkillNumber = (currentSelectSkillNumber - 1 + playerSkillList.Count) % playerSkillList.Count;

            //UIに選択変更を通知
            skillWheelUI.RotateClockwise();
        }
    }

    private void ActivationSkill()
    {
        //スキルの発動処理
        if (Input.GetMouseButtonDown(1))
        {
            SkillDate skill = skillWheelUI.CurrentSkill;

            if (skill == null) return;

            //右クリックでスキル発動
            Debug.Log("スキル " + skill.skillName + " を発動しました！");
        }
    }

    /// <summary>
    /// スキルを装備する
    /// </summary>
    public bool EquipSkill(SkillDate skill)
    {
        //装備数制限チェック
        if (equippedSkills.Count >= 3) return false;

        //未装備リストから削除できた場合のみ装備
        if (unequippedSkills.Remove(skill))
        {
            equippedSkills.Add(skill);

            //UIを再初期化
            skillWheelUI.Initialize(equippedSkills, 0);

            return true;
        }

        return false;
    }

    /// <summary>
    /// スキルの装備を解除する
    /// </summary>
    public bool UnequipSkill(SkillDate skill)
    {
        //装備リストから削除できた場合のみ処理
        if (equippedSkills.Remove(skill))
        {
            unequippedSkills.Add(skill);

            //UIを再初期化
            skillWheelUI.Initialize(equippedSkills, 0);

            return true;
        }

        return false;
    }

    /// <summary>
    /// 指定したスキルが装備されているか判定
    /// </summary>
    public bool IsEquipped(SkillDate skill)
    {
        return equippedSkills.Contains(skill);
    }

    /// <summary>
    /// 装備中スキルリストを取得
    /// </summary>
    public List<SkillDate> GetEquippedSkills()
    {
        return equippedSkills;
    }

    /// <summary>
    /// 未装備スキルリストを取得
    /// </summary>
    public List<SkillDate> GetUnequippedSkills()
    {
        return unequippedSkills;
    }
}