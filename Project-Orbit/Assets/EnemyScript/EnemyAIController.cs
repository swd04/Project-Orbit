using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [Header("NavMeshの取得")]
    [SerializeField] public NavMeshAgent agent = null;

    [Header("プレイヤー(追う対象)")]
    [SerializeField] public Transform target = null;

    [Header("EnemyStatusの取得")]
    [SerializeField] private EnemyStatus enemyStatus = null;

    [Header("実行時のステータス")]
    [SerializeField] public float currentHp = 0f;

    [Header("EnemyDataの取得")]
    [SerializeField] private EnemyData1 enemyData = null;

    [Header("Enemyスクリプトの取得")]
    [SerializeField] private Enemy currentAction = null;

    private void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();

        // ★これ追加
        currentHp = enemyStatus.MaxHp;

        //// 初期化メソッドを置く
        //Initialize();

        // EnemyStatusの取得
        enemyStatus = GetComponent<EnemyStatus>();
        //currentAction = GetComponent<Enemy>();

        target = GameObject.FindGameObjectWithTag(TagStock.Instance.PLAYER_TAG).transform;
        InitializeTarget(target);
    }

    private void Update()
    {
        // 行動選択メソッド
        SelectAction();

        Debug.Log(agent.speed);
        Debug.Log(agent.hasPath);
        agent.SetDestination(target.position);

        if (currentAction == null)
        {
            //Debug.Log("currentActionがnull");
            return;
        }

        currentAction.Execute(this);
        //Debug.Log("target: " + target);
    }

    /// <summary>
    /// ターゲットの設定
    /// </summary>
    public void InitializeTarget(Transform target)
    {
        this.target = target;
    }

    /// <summary>
    /// 敵のステータスを初期化するメソッド
    /// </summary>
    public void GetEnemyInitialStatus(int maxLifePoint, int attackPoint, int defencePoint, float moveSpeed)
    {
        currentHp = maxLifePoint;
        agent.speed = moveSpeed;
    }

    /// <summary>
    /// 行動選択のメソッド
    /// </summary>
    //private void SelectAction()
    //{
    //    // enemyDataチェック
    //    if (enemyData == null)
    //    {
    //        Debug.LogError("EnemyDataが設定されていません");
    //        return;
    //    }

    //    // actionsチェック
    //    if (enemyData.actions == null || enemyData.actions.Count == 0)
    //    {
    //        Debug.LogError("actionsが設定されていません");
    //        return;
    //    }

    //    float bestScore = float.MinValue;
    //    Enemy bestAction = null;

    //    foreach (var action in enemyData.actions)
    //    {
    //        // nullチェック
    //        if (action == null) continue;

    //        float score = action.Evaluate(this);

    //        if (score > bestScore)
    //        {
    //            bestScore = score;
    //            bestAction = action;
    //        }
    //    }

    //    // 何も選ばれなかった場合
    //    if (bestAction == null)
    //    {
    //        return;
    //    }

    //    currentAction = bestAction;
    //}

    private void SelectAction()
    {
        if (enemyData == null)
        {
            Debug.LogError("enemyDataがnull");
            return;
        }

        Debug.Log("actions数: " + enemyData.actions.Count);

        float bestScore = float.MinValue;
        Enemy bestAction = null;

        foreach (var action in enemyData.actions)
        {
            Debug.Log("ループ入った");

            if (action == null)
            {
                Debug.LogError("action null");
                continue;
            }

            float score = action.Evaluate(this);
            Debug.Log(action.name + " score = " + score);

            if (score > bestScore)
            {
                bestScore = score;
                bestAction = action;
            }
        }

        currentAction = bestAction;
    }

    /// <summary>
    /// プレイヤーまでの距離を測るメソッド
    /// </summary>
    public float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    /// <summary>
    /// 現在の体力の割合を測るメソッド
    /// </summary>
    public float GetHpRatio()
    {
        return currentHp / enemyStatus.MaxHp;
    }

    /// <summary>
    /// 敵の検知範囲を測るメソッド
    /// </summary>
    public float GetDetectionRange()
    {
        return enemyStatus.detectionRange;
    }
}
