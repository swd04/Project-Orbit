using System.Collections;
using UnityEngine;

/// <summary>
/// 敵の突進処理を実行するクラス
/// </summary>
public class EnemyClash : MonoBehaviour
{
    [Header("チャージ時間")]
    [SerializeField] private float chargeTime = 0f;

    [Header("Rigidbodyの取得")]
    [SerializeField] private Rigidbody enemyRigidbody = null;

    [Header("突進速度(何倍か)")]
    [SerializeField] private float clashSpeedMultiplier = 0.0f;

    [SerializeField] private AttackAction attackAction = null;




    private void Update()
    {
        if (attackAction.canAttack)
        {
            StartCoroutine(ChargeCoroutine());
        }
    }




    /// <summary>
    /// 突進処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator ChargeCoroutine()
    {
        yield return new WaitForSeconds(chargeTime);

        enemyRigidbody.AddForce(transform.forward * clashSpeedMultiplier, ForceMode.Impulse);

        attackAction.canAttack = false;
    }
}
