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

    [Header("UŒ‚‚Å‚«‚é‚©‚Ç‚¤‚©‚Ì”»’è")]
    [SerializeField] public bool isCanAttack = false;

    [Header("“G‚ğæ“¾")]
    [SerializeField] public NavMeshAgent agent = null;

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

        agentSpeed = enemy.agent.speed;

        if (distance > attackRange)
        {

            enemy.agent.isStopped = false;
            

            Debug.Log("UŒ‚”ÍˆÍŠO");
            return;
        }

        isCanAttack = true;

        Debug.Log("UŒ‚");
    }



}
