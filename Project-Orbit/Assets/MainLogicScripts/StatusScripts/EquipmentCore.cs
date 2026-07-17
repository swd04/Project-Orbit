using UnityEngine;
using System.Collections.Generic;

public class EquipmentCore : MonoBehaviour
{
    [Header("装備中のコア")]
    [SerializeField] public List<SoulCore> equipmentCore = new List<SoulCore>();

    [Header("プレイヤーのステータス")]
    [SerializeField] private PlayerStatus playerStatus = null;

    [Header("プレイヤーの攻撃クラス")]
    [SerializeField] private PlayerAttack playerAttack = null;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        for (int i = 0; i < equipmentCore.Count; i++)
        {
            if(equipmentCore[i] != null)
            {
                if (equipmentCore[i].coreType == CoreType.Passive)
                {
                    switch (equipmentCore[i].coreId)
                    {
                        case CoreID.None:break;
                        case CoreID.RegenerationCore:playerStatus.isRegenerationTrigger = true;
                             playerStatus.regeneCoreLevel = equipmentCore[i].soulLevel; break;
                        case CoreID.EncahntAttackCore:playerAttack.isEnchantAttackCoreSet = true; break;
                        case CoreID.EnchantMoveSpeedCore:break;
                    }
                }
                else
                {
                    playerStatus.isRegenerationTrigger = false;
                    playerAttack.isEnchantAttackCoreSet = false;
                }
            }
            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// パッシブコアを装備する処理
    /// </summary>
    public void Equip(SoulCore core)
    {
        //コアが存在しない、または既に装備済みなら終了
        if (core == null || equipmentCore.Contains(core))
        {
            return;
        }

        //装備中リストへ追加
        equipmentCore.Add(core);

        //パッシブ効果を更新
        RefreshPassive();
    }

    /// <summary>
    /// パッシブコアを解除する処理
    /// </summary>
    public void Unequip(SoulCore core)
    {
        //コアが存在しない場合は終了
        if (core == null)
        {
            return;
        }

        //装備中リストから削除
        equipmentCore.Remove(core);

        //パッシブ効果を更新
        RefreshPassive();
    }

    /// <summary>
    /// 現在装備中のパッシブ効果を更新する処理
    /// </summary>
    private void RefreshPassive()
    {
        //一度すべてのパッシブ効果を解除
        playerStatus.isRegenerationTrigger = false;
        playerAttack.isEnchantAttackCoreSet = false;

        //装備中のコアを確認してパッシブ効果を反映
        foreach (SoulCore core in equipmentCore)
        {
            switch (core.coreId)
            {
                //自動回復コア
                case CoreID.RegenerationCore:
                    playerStatus.isRegenerationTrigger = true;
                    break;

                //攻撃強化コア
                case CoreID.EncahntAttackCore:
                    playerAttack.isEnchantAttackCoreSet = true;
                    break;

                //移動速度強化コア
                case CoreID.EnchantMoveSpeedCore:
                    break;
            }
        }
    }
}