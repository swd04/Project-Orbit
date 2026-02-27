using UnityEngine;

/// <summary>
/// スキルホイールの見た目を担当するクラス
/// </summary>
public class SkillWheelView : MonoBehaviour
{
    [Header("スキルスロットUI")]

    //現在選択中のスキル表示用スロット
    [SerializeField] private SkillSlotUI skillSlotCurrent = null;

    //次のスキル表示用スロット
    [SerializeField] private SkillSlotUI skillSlotNext = null;

    //前のスキル表示用スロット
    [SerializeField] private SkillSlotUI skillSlotPrevious = null;

    [Header("スロット配置位置")]

    //現在選択中のスキルスロットの表示位置
    [SerializeField] private Transform posCurrentSkillSlot = null;

    //次スキルスロットの表示位置
    [SerializeField] private Transform posNextSkillSlot = null;

    //前スキルスロットの表示位置
    [SerializeField] private Transform posPrevSkillSlot = null;

    /// <summary>
    /// モデルの状態を元にスキルホイールの表示を更新する処理
    /// </summary>
    public void Refresh(SkillWheelModel model)
    {
        //登録されているスキル数を取得
        int count = model.Count;

        //一旦すべてのスロットを非表示にする
        skillSlotCurrent.gameObject.SetActive(false);
        skillSlotNext.gameObject.SetActive(false);
        skillSlotPrevious.gameObject.SetActive(false);

        //スキルが0個ならすべて非表示
        if (count == 0) return;

        //現在選択中スキルの表示
        skillSlotCurrent.gameObject.SetActive(true);
        skillSlotCurrent.transform.position = posCurrentSkillSlot.position;
        skillSlotCurrent.Set(model.Skills[model.CurrentIndex], true);

        //スキルが１個ならここで終了
        if (count == 1) return;

        //次スキルの表示
        skillSlotNext.gameObject.SetActive(true);
        skillSlotNext.transform.position = posNextSkillSlot.position;
        skillSlotNext.Set(model.Skills[model.NextIndex], false);

        //スキルが3つ以上ある場合のみ前スキルを表示
        if (count >= 3)
        {
            skillSlotPrevious.gameObject.SetActive(true);
            skillSlotPrevious.transform.position = posPrevSkillSlot.position;
            skillSlotPrevious.Set(model.Skills[model.PrevIndex], false);
        }
    }

    /// <summary>
    /// 現在選択中のスキルスロットのTransformを取得する
    /// </summary>
    public Transform CurrentSkillSlot =>
    skillSlotCurrent.gameObject.activeSelf ? skillSlotCurrent.transform : null;

    /// <summary>
    /// 次スキルスロットのTransformを取得する
    /// </summary>
    public Transform NextSkillSlot =>
    skillSlotNext.gameObject.activeSelf ? skillSlotNext.transform : null;

    /// <summary>
    /// 前スキルスロットのTransformを取得する
    /// </summary>
    public Transform PreviousSkillSlot =>
    skillSlotPrevious.gameObject.activeSelf ? skillSlotPrevious.transform : null;

    /// <summary>
    /// 現在選択中のスキルスロットの基準表示位置
    /// </summary>
    public Vector3 PosCurrent => posCurrentSkillSlot.position;

    /// <summary>
    /// 次スキルスロットの基準表示位置
    /// </summary>
    public Vector3 PosNext => posNextSkillSlot.position;

    /// <summary>
    /// 前スキルスロットの基準表示位置
    /// </summary>
    public Vector3 PosPrev => posPrevSkillSlot.position;
}