using UnityEngine;

public class DamageManager : SingletonBehaviour<DamageManager>
{
    [Header("UŒ‚—Í‚ð•ÛŽ‚·‚é•Ï”")]
    [SerializeField] private int attackPower = 0;

    public void GetEnemyPower(int damage)
    {
        attackPower = damage;
    }

    public int PlayerDamageCalculation(int hp)
    {
        if (hp > 0)
        {
            hp = hp - attackPower;
        }
        return hp;
    }


}
