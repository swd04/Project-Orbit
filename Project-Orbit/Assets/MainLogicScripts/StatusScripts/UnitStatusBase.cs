using UnityEngine;

/// <summary>
/// ユニットのステータスのベースクラス
/// </summary>
public abstract class UnitStatusBase : MonoBehaviour
{
    [Header("ユニットの体力")]
    [SerializeField] protected int unitLifePoint = 0;

    [Header("ユニットの攻撃力")]
    [SerializeField] protected int unitAttackPoint = 0;

    [Header("ユニットの防御力")]
    [SerializeField] protected int unitDefencePoint = 0;

    [Header("ユニットの移動速度")]
    [SerializeField] protected float moveSpeed = 0.0f;

    [Header("ユニットのタイプ")]
    [SerializeField] protected UnitType unitTypes = UnitType.None;

　　/// <summary>
  /// ユニットのタイプを示すenum
  /// </summary>
    protected enum UnitType:int
    {
        [Tooltip("値なし")] None = -1,
        [Tooltip("プレイヤー")]Player = 0,
        [Tooltip("敵")] Enemy = 1,
    }

}