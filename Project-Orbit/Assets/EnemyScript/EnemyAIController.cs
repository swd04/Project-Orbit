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

    [Header("攻撃フラグ")]
    [SerializeField] public bool isAttack = false;

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
    /// 条件によって、優先される行動が変わる
    /// その行動を選ぶ処理
    /// </summary>
    private void SelectAction()
    {
        if (enemyData == null)
        {
            Debug.LogError("enemyDataがnull");
            return;
        }


        // 最少の数を入れる
        float bestScore = float.MinValue;

        // 行動をリセット
        Enemy bestAction = null;

        // エネミーデータ内の行動をループして優先度の高いものを選ぶ
        foreach (var action in enemyData.actions)
        {
            
            if (action == null)
            {
                Debug.LogError("action null");
                continue;
            }

            // 優先度を入れる
            float score = action.Evaluate(this);

            // 入れた優先度を比較して、高かったら入れる
            if (score > bestScore)
            {
                // 優先度を更新
                bestScore = score;

                // 行動を更新
                bestAction = action;
            }
        }

        // 現在の行動として保持
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
