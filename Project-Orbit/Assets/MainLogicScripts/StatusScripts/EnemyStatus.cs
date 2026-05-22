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

    [Header("ダメージを受けた判定")]
    [SerializeField] private bool isDamaged = false;

    [SerializeField] private PhaseController phase = null;

    [SerializeField] private DamageManager damageManager = null;

    [Header("ステータスの強化値のデータ")]
    [SerializeField] private EnemyStatusBounus statusBounus = null;

    [Header("コアのプレハブ")]
    [SerializeField] private SoulCore soulCore = null;

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

        // ダメージを受ける処理
        if (isDamaged)
        {
            int damage = DamageManager.Instance.EnemyDamageCalculation(unitLifePoint);
            unitLifePoint = damage;
        }

        //ここがちえぐ
        //enemyAIController.GetEnemyInitialStatus(unitLifePoint, unitAttackPoint, unitDefencePoint, moveSpeed);

    }
    public void PhaseControllerSet(PhaseController phase)
    {
        this.phase = phase;
    }

    public void DamageManagerSet(DamageManager damageManager)
    {
        // ダメージマネージャーの設定処理をここに追加
        this.damageManager = damageManager;
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagStock.Instance.WEAPON_TAG))
        {
            isDamaged = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagStock.Instance.WEAPON_TAG))
        {
            isDamaged = false;
        }
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

