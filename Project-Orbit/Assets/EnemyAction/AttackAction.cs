using UnityEngine;

/// <summary>
/// UŒ‚ƒNƒ‰ƒX
/// </summary>
/// 
[CreateAssetMenu(menuName = "AI/Action/Attack")]
public class AttackAction : Enemy
{
    [Header("“G‚ÌUŒ‚ƒAƒNƒVƒ‡ƒ“‚ª‹N‚«‚é”ÍˆÍ")]
    [SerializeField] public float attackRange = 0f;

    [Header("“G‚ÌUŒ‚ƒXƒRƒA")]
    [SerializeField] public float score = 0f;

    [Header("UŒ‚‚Å‚«‚é‚©‚Ç‚¤‚©‚Ì”»’è")]
    [SerializeField] public bool canAttack = false;

    public override float Evaluate(EnemyAIController enemy)
    {
        if (enemy.DistanceToTarget() <= attackRange)
        {
            // UŒ‚”ÍˆÍ“à‚É‚¢‚éê‡AƒXƒRƒA‚ğŒvZ‚µ‚Ä•Ô‚·
            // ”š‚Í‰¼
            return score + (10.0f - enemy.DistanceToTarget());
        }

        return 0.0f;
    }

    public override void Execute(EnemyAIController enemy)
    {
        float distance = enemy.DistanceToTarget();

        if (distance > attackRange)
        {
            canAttack = false;
            enemy.agent.isStopped = false;
            return;
        }

        canAttack = true;
        enemy.agent.isStopped = true;
        Debug.Log("UŒ‚");
    }

    
    
}
