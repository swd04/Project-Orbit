using UnityEngine;

/// <summary>
/// プレイヤーを追うクラス
/// </summary>
public class ChaseAction : Enemy
{
    [Header("敵の移動スコア")]
    [SerializeField] public float score = 0f;

    public override float Evaluate(EnemyAIController enemy)
    {
        float distance = enemy.DistanceToTarget();

        if(distance < enemy.GetDetectionRange())
        {
            return score;
        }

        return 0.0f;
    }

    public override void Execute(EnemyAIController enemy)
    {
        enemy.agent.SetDestination(enemy.target.position);
    }
}
