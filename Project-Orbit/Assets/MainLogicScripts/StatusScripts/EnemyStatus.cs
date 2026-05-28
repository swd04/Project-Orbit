using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatus : UnitStatusBase
{
    [Header("敵のタイプ")]
    [SerializeField] private EnemyType enemyType = EnemyType.None;

    [Header("敵がプレイヤーを感知する距離")]
    [SerializeField] public float detectionRange = 0f;

    //[Header("ダメージを受けた判定")]
    //[SerializeField] private bool isDamaged = false;

    [SerializeField] private PhaseController phase = null;

    [SerializeField] private DamageManager damageManager = null;

    [Header("ステータスの強化値のデータ")]
    [SerializeField] private EnemyStatusBounus statusBounus = null;

    [Header("コアのプレハブ")]
    [SerializeField] private SoulCore soulCore = null;

    [Header("敵HPバー")]
    [SerializeField] private EnemyHPBarUI enemyHpBar = null;

    [Header("最大HP")]
    [SerializeField] private int maxHp = 0;

    /// <summary>
    /// 最大HP取得
    /// </summary>
    public int MaxHp => maxHp;

    // 最大体力
    //public int maxHp => unitLifePoint;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //現在HPを最大HPとして保存
        maxHp = unitLifePoint;

        //HPバー初期化
        if (enemyHpBar != null)
        {
            enemyHpBar.Initialize(maxHp);
        }
    }

    private void Update()
    {
        //if (unitLifePoint <= 0)
        //{
        //    phase.PhaseEnemyRemove(this);
        //    SoulCore dropSoul = Instantiate(soulCore, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //}

        DamageManager.Instance.GetEnemyPower(unitAttackPoint);

        //// ダメージを受ける処理
        //if (isDamaged)
        //{
        //    int damage = DamageManager.Instance.EnemyDamageCalculation(unitLifePoint);
        //    unitLifePoint = damage;
        //}

        //ここがちえぐ
        //enemyAIController.GetEnemyInitialStatus(unitLifePoint, unitAttackPoint, unitDefencePoint, moveSpeed);

    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void Damage()
    {
        //プレイヤー攻撃力を取得
        int damage = DamageManager.Instance.EnemyDamageCalculation();

        Debug.Log("敵が受けるダメージ : " + damage);

        //HP減少
        unitLifePoint -= damage;

        Debug.Log("敵の現在HP : " + unitLifePoint);

        //HPが0未満にならないよう制限
        unitLifePoint = Mathf.Max(unitLifePoint, 0);

        // HPバー更新
        if (enemyHpBar != null)
        {
            enemyHpBar.UpdateHP(unitLifePoint);
        }

        //HP0以下で死亡
        if (unitLifePoint <= 0)
        {
            //フェーズから削除
            phase.PhaseEnemyRemove(this);

            //ソウル生成
            Instantiate(
                soulCore,
                transform.position,
                transform.rotation);

            //敵削除
            Destroy(gameObject);
        }
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

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(TagStock.Instance.WEAPON_TAG))
    //    {
    //        isDamaged = true;
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag(TagStock.Instance.WEAPON_TAG))
    //    {
    //        isDamaged = false;
    //    }
    //}
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