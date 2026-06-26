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
                        case CoreID.RegenerationCore:playerStatus.isRegenerationTrigger = true; break;
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
}
