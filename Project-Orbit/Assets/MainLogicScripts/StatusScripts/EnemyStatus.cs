using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatus : UnitStatusBase
{
    [Header("敵のタイプ")]
    [SerializeField] private EnemyType enemyType = EnemyType.None;

    [Header("どんな行動をするのか設定をする")]
    [SerializeField] public List<Enemy> actions = new List<Enemy>();

    [Header("敵がプレイヤーを感知する距離")]
    [SerializeField] public float detectionRange = 0f;

    [Header("EnemyAIControllerの取得")]
    [SerializeField] private EnemyAIController enemyAIController = null;

    [SerializeField] private PhaseController phase = null;

    [Header("ステータスの強化値のデータ")]
    [SerializeField] private EnemyStatusBounus statusBounus = null;

    [Header("コアのプレハブ")]
    [SerializeField] private SoulCore soulCore = null;

    [Header("Control Playerの取得")]
    [SerializeField] private ControlPlayer controlPlayer = null;

    // 最大体力
    public int maxHp => unitLifePoint;

    private void Update()
    {
        if (unitLifePoint <= 0)
        {
            phase.PhaseEnemyRemove(this);
            SoulCore dropSoul = Instantiate(soulCore, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        DamageManager.Instance.GetEnemyPower(unitAttackPoint);

        int damage = DamageManager.Instance.EnemyDamageCalculation(unitLifePoint);
        unitLifePoint = damage;

        //ここがちえぐ
        //enemyAIController.GetEnemyInitialStatus(unitLifePoint, unitAttackPoint, unitDefencePoint, moveSpeed);

    }
    public void PhaseControllerSet(PhaseController phase)
    {
        this.phase = phase;
    }

    public void EnemyPhaseStatusBounus()
    {
        if (statusBounus != null)
        {
            unitLifePoint += statusBounus.bounusLifePoint;
            unitAttackPoint += statusBounus.bounusAttackPoint;
            unitDefencePoint += statusBounus.bounusDefencePoint;
            moveSpeed += statusBounus.bounusMoveSpeed;
        }

    }

    public void PlayerEnemyEating()
    {
    }

    
}


/// <summary>
/// 敵のタイプ
/// </summary>
public enum EnemyType : int
{
    None = -1,
    EnemyType1 = 0,
    EnemyType2 = 1,
    EnemyType3 = 2,
}

