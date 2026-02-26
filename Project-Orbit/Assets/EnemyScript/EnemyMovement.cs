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

    [Header("個別に設定する敵の速さ")]
    [SerializeField] private float enemySpeed = 0.0f;

    [Header("敵とプレイヤーの距離")]
    [SerializeField] private float stopDistance = 0.0f;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }
}
