using UnityEngine;

public class DamageManager : SingletonBehaviour<DamageManager>
{
    [Header("UŒ‚—Í‚ğ•Û‚·‚é•Ï”")]
    [SerializeField] private int attackPower = 0;

    public void GetDamage(int damage)
    {
        attackPower = damage;
    }

    private void DamageCalculation(int hp)
    {
        hp = hp - attackPower;
    }
}
