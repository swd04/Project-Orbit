using UnityEngine;

public class Boss : SingletonMonoBehaviour<Boss>
{
    [Header("ボスのプレハブ")]
    [SerializeField] private GameObject bossPrefab = null;

    [Header("プレイヤーのプレハブ")]
    [SerializeField] private GameObject playerPrefab = null;

    [Header("プレイヤーとボスの距離")]
    [SerializeField] private float distanceToPlayer = 0.0f;

    [Header("薙ぎ払い発生の距離")]
    [SerializeField] private float sweepDistance = 0.0f;

    [Header("ボスのステータスパラメーター")]
    [SerializeField] private EnemyStatus bossStatus = null;

    private BossState bossState = BossState.IDLE;

    public enum BossState
    {
        IDLE,
        ATTACK,
        SPECIALk,
        DEAD
    }

    private void Update()
    {
        // ボスの死亡判定
        if (bossStatus.currentHp <= 0)
        {
            bossStatus.currentHp = 0;
            bossState = BossState.DEAD;
            return;
        }

        distanceToPlayer = Vector3.Distance(bossPrefab.transform.position, playerPrefab.transform.position);

        if (distanceToPlayer <= sweepDistance)
        {
            // 薙ぎ払い攻撃の判定（プレイヤーとの距離が±3の時）

        }

        // 流星雨の判定（プレイヤーが正面にいなく、ボスが攻撃を受けている時）
        // 真空斬の判定（±3以上離れているとき）
    }

}
