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

    [SerializeField] private AttackAction attackAction = null;

    private NavMeshAgent agent;
    private bool isAttacking = false;
    private float defaultSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultSpeed = agent.speed;
    }

    private void Update()
    {
        if (attackAction.isCanAttack && !isAttacking)
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
        isAttacking = true;

        // 溜め
        agent.isStopped = true;
        yield return new WaitForSeconds(chargeTime);

        // 前方向を固定
        Vector3 forward = transform.forward;

        // 突進開始
        agent.isStopped = false;
        agent.speed = clashSpeed;

        float timer = 0f;

        while (timer < clashDuration)
        {
            // 向いている方向に進む
            agent.Move(forward * agent.speed * Time.deltaTime);

            // 突進時間までタイムを回す
            timer += Time.deltaTime;
            yield return null;
        }

        // 元に戻す
        agent.speed = defaultSpeed;
        isAttacking = false;
        attackAction.isCanAttack = false;
    }
}