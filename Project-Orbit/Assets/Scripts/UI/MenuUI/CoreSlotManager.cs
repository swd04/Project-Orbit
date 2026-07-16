using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コアスロット全体を管理するクラス
/// </summary>
public class CoreSlotManager : MonoBehaviour
{
    [Header("コアスロット一覧")]
    [SerializeField] private CoreSlotUI[] slots = null;

    [Header("管理するコア種類")]
    [SerializeField] private CoreType coreType = CoreType.None;

    [Header("スキル管理")]
    [SerializeField] private SkillActivation skillActivation = null;

    [Header("パッシブ管理")]
    [SerializeField] private EquipmentCore equipmentCore = null;

    /// <summary>
    /// コアを空いているスロットへ追加する処理
    /// </summary>
    public bool AddCore(SoulCore core)
    {
        //コアが存在しない場合は追加しない
        if (core == null)
        {
            return false;
        }

        //管理対象外のコアなら装備しない
        if (core.coreType != coreType)
        {
            Debug.Log("この種類のコアは装備できません");
            return false;
        }

        //同じコアが既に装備されていないか確認
        foreach (CoreSlotUI slot in slots)
        {
            //同じコアが既に装備されている場合
            if (!slot.IsEmpty && slot.CurrentCore == core)
            {
                Debug.Log("このコアは既に装備されています");
                return false;
            }
        }

        //空いているスロットへセット
        foreach (CoreSlotUI slot in slots)
        {
            //空いているスロットがあればコアをセットする
            if (slot.IsEmpty)
            {
                //
                slot.SetCore(core);

                //コアの装備処理
                EquipCore(core);

                return true;
            }
        }

        Debug.Log("空いているスロットがありません");

        return false;
    }

    /// <summary>
    /// コアを前詰めに並び替える処理
    /// </summary>
    public void SortSlots()
    {
        //現在セットされているコアを一時保存
        List<SoulCore> cores = new();

        //セット中のコアを取得して全スロットを空にする
        foreach (CoreSlotUI slot in slots)
        {
            //セットされているコアをリストへ追加
            if (!slot.IsEmpty)
            {
                RemoveCore(slot.CurrentCore);
                cores.Add(slot.CurrentCore);
            }

            //スロットを空にする
            slot.Clear();
        }

        //先頭から順番にコアをセットし直す
        for (int i = 0; i < cores.Count; i++)
        {
            slots[i].SetCore(cores[i]);
            EquipCore(cores[i]);
        }
    }

    /// <summary>
    /// コア装備時の処理
    /// </summary>
    private void EquipCore(SoulCore core)
    {
        //攻撃モーションコアならスキルを装備
        if (core.coreType == CoreType.AttackMotion)
        {
            skillActivation.EquipSkill(core.Skill);
        }
        //パッシブコアならパッシブ効果を適用
        else if (core.coreType == CoreType.Passive)
        {
            equipmentCore.Equip(core);
        }
    }

    /// <summary>
    /// コア解除時の処理
    /// </summary>
    public void RemoveCore(SoulCore core)
    {
        //攻撃モーションコアならスキルを解除
        if (core.coreType == CoreType.AttackMotion)
        {
            skillActivation.UnequipSkill(core.Skill);
        }
        //パッシブコアならパッシブ効果を解除
        else if (core.coreType == CoreType.Passive)
        {
            equipmentCore.Unequip(core);
        }
    }
}