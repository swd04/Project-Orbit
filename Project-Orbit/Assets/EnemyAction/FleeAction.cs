using UnityEngine;

/// <summary>
/// プレイヤーから逃げるクラス
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Flee")]
public class FleeAction : Enemy
{
    [Header("現在の体力の割合")]
    [SerializeField] public float HpThreshold = 0.0f;

    [Header("敵の移動スコア")]
    [SerializeField] public float score = 0f;

    [Header("逃げる距離")]
    [SerializeField] public float fleeDistance = 0.0f;

    public override float Evaluate(EnemyAIController enemy)
    {
        if (enemy.GetHpRatio() < HpThreshold)
        {
            return score;
        }

        return 0.0f;
    }

    /// <summary>
    /// 敵がプレイヤーから逃げる処理を実行するメソッド
    /// </summary>
    public override void Execute(EnemyAIController enemy)
    {
        Vector3 direction = (enemy.transform.position - enemy.target.position).normalized;
        Vector3 position = enemy.transform.position + direction * fleeDistance;

        enemy.agent.isStopped = false;
        enemy.agent.SetDestination(position);
    }
}
