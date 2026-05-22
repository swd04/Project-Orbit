using UnityEngine;

public class DamageManager : SingletonBehaviour<DamageManager>
{
    [Header("敵の攻撃力を保持する変数")]
    [SerializeField] private int enemyAttackPower = 0;

    [Header("プレイヤーの攻撃力を保持する変数")]
    [SerializeField] private int playerAttackPower = 0;

    [Header("Enemyタグが付いているオブジェクトを取得")]
    [SerializeField] private GameObject enemyObject = null;



    private void Update()
    {
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");

        if (enemyObject != null)
        {
            // 敵にDamageManagerをセットする
            var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStatus>();
            enemy.DamageManagerSet(this);
        }
        else
        {
            return;
        }
    }

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
