using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClash : MonoBehaviour
{
    [Header("溜め時間")]
    [SerializeField] private float chargeTime = 0f;

    [Header("突進速度")]
    [SerializeField] private float clashSpeed = 10f;

    [Header("突進時間")]
    [SerializeField] private float clashDuration = 1f;

    [Header("EnemyAIControllerを取得")]
    [SerializeField] private EnemyAIController enemyAIController = null;

    [Header("Agentを取得")]
    [SerializeField] private NavMeshAgent agent = null;

    [Header("デフォルトの速さ")]
    [SerializeField] private float defaultSpeed = 0.0f;

    [Header("Update内で何回もコルーチンが怒らないようにするための攻撃フラグ")]
    [SerializeField] private bool isCharging = false;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (enemyAIController == null)
        {
            enemyAIController = GetComponent<EnemyAIController>();
        }

        defaultSpeed = agent.speed;
    }

    private void Update()
    {
        if (enemyAIController.isAttack && !isCharging)
        {
            StartCoroutine(ChargeCoroutine());
        }
    }

    /// <summary>
    /// 突進処理のコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator ChargeCoroutine()
    {
        isCharging = true;

        // 溜め
        agent.isStopped = true;
        yield return new WaitForSeconds(chargeTime);

        // 前方向
        Vector3 forward = transform.forward;

        // 突進先を計算
        Vector3 targetPos = transform.position + forward * clashSpeed * clashDuration;

        // NavMesh上に補正
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            targetPos = hit.position;
        }

        // 突進開始
        agent.isStopped = false;
        agent.speed = clashSpeed;
        agent.SetDestination(targetPos);

        // 突進時間待つ
        yield return new WaitForSeconds(clashDuration);

        // 元に戻す
        agent.speed = defaultSpeed;
        enemyAIController.isAttack = false;

        isCharging = false;

    }
}