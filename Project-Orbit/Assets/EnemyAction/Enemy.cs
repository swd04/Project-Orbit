using UnityEngine;

public abstract class Enemy : ScriptableObject
{
    // s“®‚Ì•]‰¿
    public abstract float Evaluate(EnemyAIController enemy);

    // ÀÛ‚Ìs“®
    public abstract void Execute(EnemyAIController enemy);

   
}
