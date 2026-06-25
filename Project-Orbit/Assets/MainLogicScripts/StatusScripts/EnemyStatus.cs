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

    [Header("現在の体力")]
    [SerializeField] public int currentHp = 0;

    //[Header("ダメージを受けた判定")]
    //[SerializeField] private bool isDamaged = false;

    [SerializeField] private PhaseController phase = null;

    [SerializeField] private DamageManager damageManager = null;

    [SerializeField] private EnemyAIController enemyAIController = null;

    [Header("ステータスの強化値のデータ")]
    [SerializeField] private EnemyStatusBounus statusBounus = null;

    [Header("コアのプレハブ")]
    [SerializeField] private SoulCore soulCore = null;

    [Header("敵用HPバークラス")]
    [SerializeField] private EnemyHPBarUI enemyHpBar = null;

    [Header("捕食された際の強化するステータスの種類")]
    [SerializeField] private PlayerEatStatus eatStatus = PlayerEatStatus.None;

    [Header("捕食された際の強化値")]
    [SerializeField] private int statusuUpPoint = 0;

    [Header("最大HP")]
    public int maxHp = 0;

    /// <summary>
    /// 最大HP取得
    /// </summary>
    //public int MaxHp => maxHp;

    // 最大体力
    //public int maxHp => unitLifePoint;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        enemyAIController = GetComponent<EnemyAIController>();

        //現在HPを最大HPとして保存
        maxHp = unitLifePoint;

        enemyAIController.agent.speed = moveSpeed;

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

        currentHp = unitLifePoint;
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
        //ダメージ前HP保存
        int beforeHp = unitLifePoint;

        //プレイヤー攻撃力を取得
        int damage = DamageManager.Instance.EnemyDamageCalculation(unitLifePoint);

        Debug.Log("敵が受けるダメージ : " + damage);

        //HP減少
        unitLifePoint = damage;

        Debug.Log("敵の現在HP : " + unitLifePoint);

        //HPが0未満にならないよう制限
        unitLifePoint = Mathf.Max(unitLifePoint, 0);

        //実際に受けたダメージ量
        int damageAmount = beforeHp - unitLifePoint;

        //Collider取得
        Collider col = GetComponent<Collider>();

        ////Colliderが取得できなかった場合の予備位置
        Vector3 damagePos = transform.position + Vector3.up * 2f;

        //Colliderが存在する場合
        if (col != null)
        {
            //Colliderの上端付近を表示位置に設定
            damagePos =
                col.bounds.center +
                Vector3.up * col.bounds.extents.y;
        }

        //ダメージ数値表示
        DamageTextManager.Instance.ShowDamage(
            damageAmount,
            damagePos
        );

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

            // 魂化で攻撃されたときはソウルを生成
            if (PlayerChoseAttackMode.Instance.currentAttackMode == PlayerChoseAttackMode.AttackMode.SOULREINFORCE)
            {
                //ソウル生成
                Instantiate(soulCore, transform.position, transform.rotation);
            }
            else
            {
                switch (eatStatus)
                {
                    case PlayerEatStatus.None: break;
                    case PlayerEatStatus.LifePoint: phase.playerStatus.EnchantStatus(statusuUpPoint, 0, 0, 0.0f); break;
                    case PlayerEatStatus.AttackPoint: phase.playerStatus.EnchantStatus(0, statusuUpPoint, 0, 0.0f); break;
                    case PlayerEatStatus.DefencePoint: phase.playerStatus.EnchantStatus(0, 0, statusuUpPoint, 0.0f); break;
                }

            }

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

public enum PlayerEatStatus : int
{
    None = -1,
    LifePoint = 0,
    AttackPoint = 1,
    DefencePoint = 2,
}