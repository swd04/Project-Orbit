using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵全般に共通する移動処理するクラス
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("プレイヤーオブジェクトの取得")]
    [SerializeField] private GameObject playerObject = null;

    [Header("Agentの取得")]
    [SerializeField] private NavMeshAgent enemyAgent = null;

   

    [Header("武器のオブジェクト取得")]
    [SerializeField] GameObject weaponObject = null;

    [Header("武器のコライダー取得")]
    [SerializeField] Collider weaponCollider = null;

    [Header("個別に設定する敵の速さ")]
    [SerializeField] private float enemySpeed = 0.0f;

    [Header("敵とプレイヤーの距離")]
    [SerializeField] private float stopDistance = 0.0f;

    [Header("攻撃判定")]
    [SerializeField] private bool isAttack = false;

    [Header("武器の回転角度")]
    [SerializeField] private Vector3 rotateAngle = Vector3.zero;

    [Header("攻撃の回転の速さ")]
    [SerializeField] private float rotateSpeed = 0.0f;


    private void Update()
    {
        EnemyChasePlayer();
        EnemySpeed();
        EnemyStopDistance();
    }

    /// <summary>
    /// 敵がプレイヤーを追う処理を行うメソッド
    /// </summary>
    private void EnemyChasePlayer()
    {
        // エリア内に入った時の判定が必要
        // 敵がプレイヤーを追う処理
        enemyAgent.SetDestination(playerObject.transform.position);

    }

    /// <summary>
    /// 敵の速さを入力値に設定するメソッド
    /// </summary>
    private void EnemySpeed()
    {
        // 敵によって変える
        enemyAgent.speed = enemySpeed;
    }

    /// <summary>
    /// 敵がプレイヤーの前で止まる距離を設定するメソッド
    /// </summary>
    private void EnemyStopDistance()
    {
        // 敵によって変える
        enemyAgent.stoppingDistance = stopDistance;

        if (!isAttack)
        {
            // 敵とプレイヤーの距離がstopDistance以下になったら攻撃する処理
            if (enemyAgent.remainingDistance <= stopDistance)
            {

                isAttack = true;
                EnemyAttack();

            }
        }
    }

    private void EnemyAttack()
    {
        // 敵が攻撃する処理
        weaponObject.transform.DOLocalRotate(new Vector3(rotateAngle.x, rotateAngle.y, rotateAngle.z), rotateSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            weaponObject.transform.DOLocalRotate(Vector3.zero, rotateSpeed).SetEase(Ease.Linear);

            isAttack = false;

            // コライダーを無効

            if (weaponCollider != null)
            {
                weaponCollider.enabled = false;
            }

        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
