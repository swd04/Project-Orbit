using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [Header("NavMesh")]
    [SerializeField] public NavMeshAgent agent = null;

    [Header("プレイヤー(追う対象)")]
    [SerializeField] public Transform target = null;

    [Header("敵のデータ")]
    [SerializeField] private EnemyData1 enemyData = null;

    [Header("実行時のステータス")]
    [SerializeField] public float currentHp = 0f;

    [Header("Enemyスクリプトの取得")]
    [SerializeField] private Enemy currentAction = null;

    private void Start()
    {
        // 初期化メソッドを置く
        Initialize();
    }

    private void Update()
    {
        // 行動選択メソッド
        SelectAction();
        currentAction?.Execute(this);
    }

    /// <summary>
    /// 値を初期化するメソッド
    /// 敵のパラメーターを追加したら、必ずここで初期化する
    /// </summary>
    private void Initialize()
    {
        currentHp = enemyData.maxHp;
        agent.speed = enemyData.moveSpeed;
    }

    private void SelectAction()
    {
        float bestScore = float.MinValue;
        Enemy bestAction = null;

        foreach(var action in enemyData.actions)
        {
            float score = action.Evaluate(this);
            
            if(score > bestScore)
            {
                bestScore = score;
                bestAction = action;
            }
        }

        currentAction = bestAction;
    }

    public float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    public float GetHpRatio()
    {
        return currentHp / enemyData.maxHp;
    }

    public float GetDetectionRange()
    {
        return enemyData.detectionRange;
    }
}
