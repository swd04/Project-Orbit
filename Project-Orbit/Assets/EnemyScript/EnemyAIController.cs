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

        Debug.Log(agent.speed + "ここはEnemyAIControllerクラス");

        //// 初期化メソッドを置く
        //Initialize();


        //currentAction = GetComponent<Enemy>();

        target = GameObject.FindGameObjectWithTag(TagStock.Instance.PLAYER_TAG).transform;
        InitializeTarget(target);
    }

    private void Update()
    {
        currentHp = enemyStatus.currentHp;

        // 行動選択メソッド
        SelectAction();

 
        //agent.SetDestination(target.position);

        if (currentAction == null)
        {

            return;
        }

        currentAction.Execute(this);

    }

    /// <summary>
    /// ターゲットの設定
    /// </summary>
    public void InitializeTarget(Transform target)
    {
        this.target = target;
    }



    /// <summary>
    /// 
    /// </summary>
    private void SelectAction()
    {
        if (enemyData == null)
        {
            Debug.LogError("enemyDataがnull");
            return;
        }



        float bestScore = float.MinValue;
        Enemy bestAction = null;

        foreach (var action in enemyData.actions)
        {

            if (action == null)
            {
                Debug.LogError("action null");
                continue;
            }

            float score = action.Evaluate(this);


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
        return currentHp / enemyStatus.maxHp;
    }

    /// <summary>
    /// 敵の検知範囲を測るメソッド
    /// </summary>
    public float GetDetectionRange()
    {
        return enemyStatus.detectionRange;
    }


}
