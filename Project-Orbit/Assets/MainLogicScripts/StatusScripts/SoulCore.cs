using UnityEngine;
using System.Collections.Generic;

public class SoulCore : MonoBehaviour
{
    public enum ActionType
    {
        None = -1,
        Type1 = 0,
        Type2 = 1,
        Type3 = 2,
        Type4 = 3,
        Type5 = 4,
    }

    [SerializeField] public ActionType actionType = ActionType.None;

    [SerializeField] private int actionAttackPoint = 0;

    [SerializeField] public int soulLevel = 0;

    [SerializeField] public CoreType coreType = CoreType.None;

    [SerializeField] public CoreID coreId = CoreID.None;

    [Header("このコアのスキル")]
    [SerializeField] private SkillDate skill = null;

    [Header("コアの名前")]
    [SerializeField] public string coreName = "";

    [Header("コアがレベルアップに必要な回収数のリスト")]
    [SerializeField] private List<int> coreLevelUpCountList = new List<int>();

    [Header("コアの取得数")]
    [SerializeField] private int coreGetCount = 0;

    /// <summary>
    /// このコアが持つスキル
    /// </summary>
    public SkillDate Skill => skill;

    public void SoulLevelUp()
    {
        coreGetCount++;
        if (coreGetCount > coreLevelUpCountList[soulLevel])
        {
            soulLevel++;
        }
        
    }

}

public enum CoreType
{
    None,
    [Tooltip("パッシブタイプのコア")]Passive,
    [Tooltip("攻撃モーション追加のコア")]AttackMotion
}

public enum CoreID
{
    None,
    [Tooltip("自動回復コア")]RegenerationCore,
    [Tooltip("確率攻撃強化コア")]EncahntAttackCore,
    [Tooltip("移動速度強化コア")]EnchantMoveSpeedCore,

    [Tooltip("スラッシュウェーブコア")] SlashWaveCore
}