using UnityEngine;

/// <summary>
/// 攻撃クラス
/// </summary>
public class AttackAction8 : Enemy
{
    [Header("敵の攻撃アクションが起きる範囲")]
    [SerializeField] public float attackRange = 0f;

    [Header("敵の攻撃スコア")]
    [SerializeField] public float score = 0f;



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


        // 攻撃回数がクールタイム発生回数を超えていない場合、攻撃を行う

        enemy.agent.ResetPath();
        Debug.Log("攻撃");

    }
}
