using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerStatus : UnitStatusBase
{
    [SerializeField] private PlayerChoseAttackMode playerChoseAttackMode = null;

    [SerializeField] public List<SoulCore> soulCoresList = new List<SoulCore>();

    [SerializeField] private List<int> soulLevel = new List<int>();

    [SerializeField] private ControlPlayer controlPlayer = null;

    [SerializeField] public int maxHp = 0;

    [Header("ダメージを受けた判定")]
    [SerializeField] public bool isDamage = false;

    [Header("自動回復が有効かどうか")]
    [SerializeField] public bool isRegenerationTrigger = false;

    [Header("自動回復の間隔")]
    [SerializeField] private float regenerationTime = 0.0f;

    [Header("自動回復の経過時間")]
    [SerializeField] private float regenerationDelta = 0.0f;

    [Header("児童会h区が有効な体力のパーセント")]
    [SerializeField] private float regenerationTriggerRatio = 0.0f;

    [Header("現在の体力のパーセント")]
    [SerializeField] private float lifePointRatio = 0.0f;

  

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
        if (soulLevel.Count < soulCoresList.Count)
        {
            soulLevel.Add(0);
        }

        for (int i = 0; i < soulLevel.Count; i++)
        {
            if (soulCoresList[i] != null)
            {
                soulLevel[i] = soulCoresList[i].soulLevel;
            }

        }

        DamageManager.Instance.GetPlayerPower(unitAttackPoint);

        //Debug.LogFormat("プレイヤーのHP{0}です", unitLifePoint);

        if (isDamage)
        {
            int damage = DamageManager.Instance.PlayerDamageCalculation(unitLifePoint);

            unitLifePoint -= damage;
        }

        RegenerationLifePoint();

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
            if(lifePointRatio <= regenerationTriggerRatio)
            {
                regenerationDelta += Time.deltaTime;
                if(regenerationDelta > regenerationTime)
                {
                    unitLifePoint += maxHp / 100;
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

    /// <summary>
    /// ソウルコアを登録する処理
    /// </summary>
    public void AddSoulCore(SoulCore soulCore)
    {
        //同じ種類のコアを所持しているか確認
        foreach (SoulCore core in soulCoresList)
        {
            //同じ種類ならレベルアップして回収
            if (core.actionType == soulCore.actionType)
            {
                core.SoulLevelUp();

                //回収したコアを非表示
                soulCore.gameObject.SetActive(false);

                return;
            }
        }

        //初めて取得した種類なので登録
        soulCoresList.Add(soulCore);

        //回収したコアを非表示
        soulCore.gameObject.SetActive(false);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagStock.Instance.ENEMY_WEAPON_TAG))
        {
            isDamage = false;
        }
    }

    
}