using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



public class PlayerStatus : UnitStatusBase
{
    [SerializeField] public List<SoulCore> soulCoresList = new List<SoulCore>();

    [SerializeField] private List<int> soulLevel = new List<int>();

    [SerializeField] private ControlPlayer controlPlayer = null;

    [SerializeField] public int maxHp = 0;




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

        Debug.LogFormat("プレイヤーのHP{0}です", unitLifePoint);

        if (Input.GetKeyDown(KeyCode.T))
        {
            int damage = DamageManager.Instance.PlayerDamageCalculation(unitLifePoint);
            unitLifePoint = damage;
        }

    }



    public void EnchantStatus(int hp, int attack, int defence, float speed)
    {
        unitLifePoint += hp;
        unitAttackPoint += attack;
        unitDefencePoint += defence;
        moveSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            // UnitManager.Instance.PhaseStart();
        }

        //if (other.CompareTag("EnemyWeapon") || Input.GetKeyDown(KeyCode.T))
        //{
        //    int damage = DamageManager.Instance.PlayerDamageCalculation(unitLifePoint);
        //    unitLifePoint -= damage;
        //}

        if (other.CompareTag("Soul"))
        {
            int soulNotGetCount = 0;
            var getSoul = other.gameObject.GetComponent<SoulCore>();
            if (soulCoresList != null)
            {
                if (soulCoresList[0] != null)
                {
                    for (int i = 0; i < soulCoresList.Count; i++)
                    {
                        if (getSoul.actionType == soulCoresList[i].actionType)
                        {
                            soulCoresList[i].SoulLevelUp();
                            getSoul.gameObject.SetActive(false);
                        }
                        else
                        {
                            soulNotGetCount++;
                        }
                    }

                    if (soulNotGetCount >= soulCoresList.Count)
                    {
                        soulCoresList.Add(getSoul);
                    }
                }
                else
                {
                    soulCoresList[0] = getSoul;
                    getSoul.gameObject.SetActive(false);
                }
            }
        }
    }
}
