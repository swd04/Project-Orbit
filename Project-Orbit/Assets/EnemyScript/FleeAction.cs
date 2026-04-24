using UnityEngine;

public class FleeAction : Enemy
{
    [Header("Œ»İ‚Ì‘Ì—Í‚ÌŠ„‡")]
    [SerializeField] public float HpThreshold = 0.0f;

    [Header("“G‚ÌˆÚ“®ƒXƒRƒA")]
    [SerializeField] public float score = 0f;

    [Header("“¦‚°‚é‹——£")]
    [SerializeField] public float fleeDistance = 0.0f;

    public override float Evaluate(EnemyAIController enemy)
    {
        if(enemy.GetHpRatio() < HpThreshold)
        {
            return score;
        }

        return 0.0f;
    }

    public override void Execute(EnemyAIController enemy)
    {
        Vector3 direction = (enemy.transform.position - enemy.target.position).normalized;
        Vector3 position = enemy.transform.position + direction * fleeDistance;
    }
}
