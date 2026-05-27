using UnityEngine;

/// <summary>
/// 攻撃クラス
/// </summary>
/// 
[CreateAssetMenu(menuName = "AI/Action/Attack")]
public class AttackAction : Enemy
{
    [Header("敵の攻撃アクションが起きる範囲")]
    [SerializeField] public float attackRange = 0f;

    [Header("敵の攻撃スコア")]
    [SerializeField] public float score = 0f;

    public override float Evaluate(EnemyAIController enemy)
    {
        if (enemy.DistanceToTarget() <= attackRange)
        {
            // 攻撃範囲内にいる場合、スコアを計算して返す
            // 数字は仮
            return score + (10.0f - enemy.DistanceToTarget());
        }

        return 0.0f;
    }

    public override void Execute(EnemyAIController enemy)
    {
        float distance = enemy.DistanceToTarget();

        if (distance > attackRange)
        {
            enemy.agent.isStopped = false;
            return;
        }

        enemy.agent.isStopped = true;
        Debug.Log("攻撃");
    }
}
