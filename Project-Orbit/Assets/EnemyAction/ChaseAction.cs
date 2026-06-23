using UnityEngine;

/// <summary>
/// プレイヤーを追うクラス
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Chase")]
public class ChaseAction : Enemy
{
    [Header("敵の移動スコア")]
    [SerializeField] public float score = 0f;

    /// <summary>
    /// 敵の行動を評価するメソッド
    /// </summary>
    public override float Evaluate(EnemyAIController enemy)
    {
        float distance = enemy.DistanceToTarget();
        float range = enemy.GetDetectionRange();

        if (distance > range)
        {
            // 範囲外なら最優先で追う
            return 100f;
        }

        // 範囲内なら距離に応じて
        return distance;
    }

    public override void Execute(EnemyAIController enemy)
    {
        enemy.agent.isStopped = false;
        Debug.Log("今プレイヤーを追っています");
        enemy.agent.SetDestination(enemy.target.position);
    }
}
