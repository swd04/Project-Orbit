using UnityEngine;
using System.Collections.Generic;

public class PlayerStatus : UnitStatusBase
{
    [SerializeField] public List<SoulCore> soulCoresList = new List<SoulCore>();

    [SerializeField] private List<int> soulLevel = new List<int>();
    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        if(soulLevel.Count < soulCoresList.Count)
        {
            soulLevel.Add(0);
        }

        for(int i = 0; i < soulLevel.Count; i++)
        {
            if (soulCoresList[i] != null)
            {
                soulLevel[i] = soulCoresList[i].soulLevel;
            }
            
        }
    }

    public void EnchantStatus(int hp,int attack,int defence,float speed)
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

        if(other.CompareTag("Soul"))
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
