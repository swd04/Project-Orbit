using System.Collections;
using UnityEngine;

/// <summary>
/// �G�̓ːi���������s����N���X
/// </summary>
public class EnemyClash : MonoBehaviour
{
    [Header("溜め時間")]
    [SerializeField] private float chargeTime = 0f;

    [Header("Rigidbodyを設定する")]
    [SerializeField] private Rigidbody enemyRigidbody = null;

    [Header("突進速度(倍率)")]
    [SerializeField] private float clashSpeedMultiplier = 0.0f;

    [SerializeField] private AttackAction attackAction = null;




    private void Update()
    {
        if (attackAction.isCanAttack)
        {
            StartCoroutine(ChargeCoroutine());
        }
        else
        {
            enemyRigidbody.linearVelocity = Vector3.zero;
        }

        Debug.Log(attackAction.isCanAttack);
    }




    /// <summary>
    /// 突進コルーチンメソッド
    /// </summary>
    public IEnumerator ChargeCoroutine()
    {
        yield return new WaitForSeconds(chargeTime);

        enemyRigidbody.linearVelocity = transform.forward * clashSpeedMultiplier;

        Debug.Log(transform.forward + "ｇｍｆｋぉｄｈｙｊぴおｔｒ");
    }
}
