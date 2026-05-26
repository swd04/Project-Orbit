using UnityEngine;

/// <summary>
/// UŒ‚ƒNƒ‰ƒX
/// </summary>
public class AttackAction8 : Enemy
{
    [Header("“G‚ÌUŒ‚ƒAƒNƒVƒ‡ƒ“‚ª‹N‚«‚é”ÍˆÍ")]
    [SerializeField] public float attackRange = 0f;

    [Header("“G‚ÌUŒ‚ƒXƒRƒA")]
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
        enemy.agent.ResetPath();
        Debug.Log("UŒ‚");
    }
}
