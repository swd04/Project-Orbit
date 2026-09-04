using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 攻撃クラス
/// </summary>
/// 
[CreateAssetMenu(menuName = "AI/Action/Attack")]
public class AttackAction : Enemy
{
    [Header("敵の攻撃アクションが起きる範囲")]
    [SerializeField] public float attackRange = 0.0f;

    [Header("敵の攻撃スコア")]
    [SerializeField] public float score = 0.0f;

    [Header("クールタイム中かどうか")]
    [SerializeField] public bool isCoolTime = false;

    [Header("敵のクールタイムが何秒か")]
    [SerializeField] public float coolTime = 0f;

    [Header("時間")]
    [SerializeField] public float currentTime = 0f;

    [Header("何回攻撃したらクールタイムが発生するか")]
    [SerializeField] public int attackCount = 0;

    [Header("攻撃回数")]
    [SerializeField] public int currentAttackCount = 0;

    [SerializeField] public float agentSpeed = 0.0f;

    public override float Evaluate(EnemyAIController enemy)
    {
        if (enemy.DistanceToTarget() <= attackRange)
        {

            return score;
        }

        return 0.0f;
    }

    public override void Execute(EnemyAIController enemy)
    {
        Debug.Log("攻撃アクション");

        Debug.Log("攻撃回数" + currentAttackCount);

        // 攻撃回数がクールタイム発生回数を超えた場合、クールタイムを開始する
        if (attackCount <= currentAttackCount)
        {
            // 時間計測
            currentTime += Time.deltaTime;

            // クールタイム中の判定をtrueにする
            isCoolTime = true;

            Debug.Log("クールタイム中");

            // クールタイムが規定時間まで到達したら、攻撃回数と時間をリセットして、クールタイムの判定をfalseにする
            if (currentTime >= coolTime)
            {
                currentAttackCount = 0;
                currentTime = 0f;
                isCoolTime = false;

                Debug.Log("クールタイム終了");
            }

        }

        if (!isCoolTime)
        {
            float distance = enemy.DistanceToTarget();

            if (distance > attackRange)
            {
                enemy.agent.isStopped = false;
                enemy.isAttack = false;

                //Debug.Log("攻撃範囲外");
                return;
            }

            enemy.isAttack = true;

            currentAttackCount++;
            Debug.Log("攻撃");
        }
        else
        {
            return;
        }
    }
}
