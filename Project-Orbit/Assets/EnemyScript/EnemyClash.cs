using System.Collections;
using UnityEngine;

public class EnemyClash : EnemyStatus
{
    [Header("チャージ時間")]
    [SerializeField] private float chargeTime = 0f;

    [Header("Rigidbodyの取得")]
    [SerializeField] private Rigidbody enemyRigidbody = null;

    [Header("突進速度(何倍か)")]
    [SerializeField] private float clashSpeedMultiplier = 0.0f;

    private void Update()
    {

    }

    /// <summary>
    /// 突進の処理
    /// </summary>
    private void Clash()
    {

    }

    private IEnumerator ChargeCoroutine()
    {
        yield return new WaitForSeconds(chargeTime);

        enemyRigidbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }
}
