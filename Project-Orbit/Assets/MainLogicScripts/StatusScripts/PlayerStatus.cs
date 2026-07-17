using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerStatus : UnitStatusBase
{
    [SerializeField] private PlayerChoseAttackMode playerChoseAttackMode = null;

    //[SerializeField] private List<int> soulLevel = new List<int>();

    [SerializeField] private ControlPlayer controlPlayer = null;

    [SerializeField] public int maxHp = 0;

    [Header("ダメージを受けた判定")]
    [SerializeField] public bool isDamage = false;

    [Header("自動回復が有効かどうか")]
    [SerializeField] public bool isRegenerationTrigger = false;

    [Header("自動回復の間隔")]
    [SerializeField] private float regenerationTime = 0.0f;

    [Header("回復間隔のリスト")]
    [SerializeField] private List<float> levelRegenerationTime = new List<float>();

    [Header("自動回復の経過時間")]
    [SerializeField] private float regenerationDelta = 0.0f;

    [Header("オート回復が有効な体力のパーセント")]
    [SerializeField] private float regenerationTriggerRatio = 0.0f;

    [Header("レベル別オート回復有効なパーセント")]
    [SerializeField] private List<float> levelRegenerationTriggerRatioList = new List<float>();

    [Header("現在の体力のパーセント")]
    [SerializeField] private float lifePointRatio = 0.0f;

    [Header("回復する値")]
    [SerializeField] private int regenePoint = 0;

    [Header("回復する割合リスト(計算式　Max体力*％)")]
    [SerializeField] private List<float> regenePointParsentList = new List<float>();

    [Header("リジェネコアのレベル")]
    [SerializeField] public int regeneCoreLevel = 0;

    /// <summary>
    /// 現在HP
    /// </summary>
    public int Life => unitLifePoint;

    /// <summary>
    /// 最大HP
    /// </summary>
    public int MaxHP => maxHp;

    /// <summary>
    /// 攻撃力
    /// </summary>
    public int Attack => unitAttackPoint;

    /// <summary>
    /// 防御力
    /// </summary>
    public int Defence => unitDefencePoint;

    private void Start()
    {
        maxHp = unitLifePoint;
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        //if (soulLevel.Count < soulCoresList.Count)
        //{
        //    soulLevel.Add(0);
        //}

        //for (int i = 0; i < soulLevel.Count; i++)
        //{
        //    if (soulCoresList[i] != null)
        //    {
        //        soulLevel[i] = soulCoresList[i].soulLevel;
        //    }

        //}

        DamageManager.Instance.GetPlayerPower(unitAttackPoint);

        //Debug.LogFormat("プレイヤーのHP{0}です", unitLifePoint);

        if (isDamage)
        {
            int damage = DamageManager.Instance.PlayerDamageCalculation(unitLifePoint);

            unitLifePoint -= damage;
        }

        RegenerationLifePoint();

        //テスト用
        if (Input.GetKeyDown(KeyCode.Q))
        {
            unitLifePoint -= 10;
        }
    }

    public void EnchantStatus(int hp, int attack, int defence, float speed)
    {
        unitLifePoint += hp;
        unitAttackPoint += attack;
        unitDefencePoint += defence;
        moveSpeed = speed;

        //ログ表示
        if (hp > 0)
        {
            GameLogUI.Instance.AddLog($"HPが{hp}上昇した");
        }

        if (attack > 0)
        {
            GameLogUI.Instance.AddLog($"攻撃力が{attack}上昇した");
        }

        if (defence > 0)
        {
            GameLogUI.Instance.AddLog($"防御力が{defence}上昇した");
        }
    }

    /// <summary>
    /// 自動回復メソッド
    /// </summary>
    public void RegenerationLifePoint()
    {
        //現在の残存体力の比率を計算
        lifePointRatio = (float)unitLifePoint / (float)maxHp;
        
        if (isRegenerationTrigger)
        {
            regenerationTime = levelRegenerationTime[regeneCoreLevel];

            regenerationTriggerRatio = levelRegenerationTriggerRatioList[regeneCoreLevel];

            float regenePower = maxHp * regenePointParsentList[regeneCoreLevel];
            regenePoint = (int)regenePower;


            if (lifePointRatio <= regenerationTriggerRatio)
            {
                regenerationDelta += Time.deltaTime;
                if(regenerationDelta > regenerationTime)
                {
                    unitLifePoint += regenePoint;
                    regenerationDelta = 0;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other != null)
    //    {
    //        // UnitManager.Instance.PhaseStart();
    //    }

    //    //if (other.CompareTag("EnemyWeapon") || Input.GetKeyDown(KeyCode.T))
    //    //{
    //    //    int damage = DamageManager.Instance.PlayerDamageCalculation(unitLifePoint);
    //    //    unitLifePoint -= damage;
    //    //}

    //    //if (other.CompareTag(TagStock.Instance.ENEMY_WEAPON_TAG))
    //    //{
    //    //    isDamage = true;
    //    //}

    //    //if (other.CompareTag("Soul"))
    //    //{
    //    //    SoulCore getSoul = other.GetComponent<SoulCore>();

    //    //    if (getSoul == null)
    //    //    {
    //    //        return;
    //    //    }

    //    //    for (int i = 0; i < soulCoresList.Count; i++)
    //    //    {
    //    //        if (getSoul.actionType == soulCoresList[i].actionType)
    //    //        {
    //    //            soulCoresList[i].SoulLevelUp();

    //    //            getSoul.gameObject.SetActive(false);

    //    //            return;
    //    //        }
    //    //    }

    //    //    soulCoresList.Add(getSoul);

    //    //    getSoul.gameObject.SetActive(false);
    //    //}
    //}

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagStock.Instance.ENEMY_WEAPON_TAG))
        {
            isDamage = false;
        }
    }
}