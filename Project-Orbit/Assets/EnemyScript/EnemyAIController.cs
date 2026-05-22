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
        currentAction.Execute(this);
        Debug.Log(currentAction);
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
    private void SelectAction()
    {
        // いずれかの行動を確定で選択するため、低い値を入れる
        float bestScore = float.MinValue;

        // 最高のスコアを持つ行動を保存する変数
        Enemy bestAction = null;

        // 敵の行動リストをループさせて、各行動のスコアを評価する
        foreach (var action in enemyData.actions)
        {
            // スコア評価
            float score = action.Evaluate(this);

            // 現在の行動のスコアが最高のスコアよりも高い場合、最高のスコアと行動を更新する
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
