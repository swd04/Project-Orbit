using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// UŒ‚ƒNƒ‰ƒX
/// </summary>
/// 
[CreateAssetMenu(menuName = "AI/Action/Attack")]
public class AttackAction : Enemy
{
    [Header("“G‚ÌUŒ‚ƒAƒNƒVƒ‡ƒ“‚ª‹N‚«‚é”ÍˆÍ")]
    [SerializeField] public float attackRange = 0.0f;

    [Header("“G‚ÌUŒ‚ƒXƒRƒA")]
    [SerializeField] public float score = 0.0f;



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
        float distance = enemy.DistanceToTarget();

        if (distance > attackRange)
        {
            enemy.agent.isStopped = false;
            enemy.isAttack = false;

            Debug.Log("UŒ‚”ÍˆÍŠO");
            return;
        }

        enemy.isAttack = true;

        Debug.Log("UŒ‚");
    }



}
