using UnityEngine;

public class DamageManager : SingletonBehaviour<DamageManager>
{
    [Header("“G‚ÌUŒ‚—Í‚ð•ÛŽ‚·‚é•Ï”")]
    [SerializeField] private int enemyAttackPower = 0;

    [Header("ƒvƒŒƒCƒ„[‚ÌUŒ‚—Í‚ð•ÛŽ‚·‚é•Ï”")]
    [SerializeField] private int playerAttackPower = 0;

    public void GetPlayerPower(int playerpower)
    {
        playerAttackPower = playerpower;
    }

    public void GetEnemyPower(int enemyPower)
    {
        enemyAttackPower = enemyPower;
    }

    public int PlayerDamageCalculation(int playerHp)
    {
        if (playerHp > 0)
        {
            playerHp = playerHp - enemyAttackPower;
        }
        return playerHp;
    }

    public int EnemyDamageCalculation(int enemyHp)
    {
        if (enemyHp > 0)
        {
            enemyHp = enemyHp - playerAttackPower;
        }

        return enemyHp;
    }


}
