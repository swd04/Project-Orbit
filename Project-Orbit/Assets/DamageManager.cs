using System;
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
        // 
        enemyObject = GameObject.FindGameObjectWithTag(TagStock.Instance.ENEMY_TAG);

        if (enemyObject != null)
        {
            // 敵にDamageManagerをセットする
            var enemy = GameObject.FindGameObjectWithTag(TagStock.Instance.ENEMY_TAG).GetComponent<EnemyStatus>();
            enemy.DamageManagerSet(this);
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// プレイヤーの攻撃力を取得するメソッド
    /// </summary>
    public void GetPlayerPower(int playerpower)
    {
        playerAttackPower = playerpower;
    }

    /// <summary>
    /// 敵の攻撃力を取得するメソッド
    /// </summary>
    public void GetEnemyPower(int enemyPower)
    {
        enemyAttackPower = enemyPower;
    }
      
    /// <summary>
    /// プレイヤーが受けるダメージを計算するメソッド
    /// </summary>
    public int PlayerDamageCalculation(int playerHp)
    {
        if (playerHp > 0)
        {
            playerHp = playerHp - enemyAttackPower;
        }
        return playerHp;
    }

    /// <summary>
    /// 敵が受けるダメージを計算するメソッド
    /// </summary>
    public int EnemyDamageCalculation(int enemyHp)
    {
        if (enemyHp > 0)
        {
            enemyHp = enemyHp - playerAttackPower;
        }

        return enemyHp;
    }


}
