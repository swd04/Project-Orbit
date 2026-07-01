using UnityEngine;
using DG.Tweening;

/// <summary>
/// 剣でプレイヤーを攻撃する処理を行うクラス
/// </summary>
public class SkeletonEnemyController : MonoBehaviour
{
    // Aiの行動パターン選択と同期させる

    [Header("攻撃の回転の速さ")]
    [SerializeField] private float rotateSpeed = 0.0f;

    [Header("武器の回転角度")]
    [SerializeField] private Vector3 rotateAngle = Vector3.zero;

    [Header("武器のオブジェクト")]
    [SerializeField] private GameObject weaponObject = null;

    [Header("武器のコライダーの取得")]
    [SerializeField] private Collider weaponCollider = null;

    [Header("攻撃判定")]
    [SerializeField] private bool isAttack = false;

    [Header("EnemyAIControllerの取得")]
    [SerializeField] private EnemyAIController enemyAIController = null;

    private void Start()
    {
        if (weaponObject == null)
        {
            Debug.LogError(weaponObject + "がnullです。");
        }
        if (weaponCollider == null)
        {
            Debug.LogError(weaponCollider + "がnullです。");
        }
        if (enemyAIController == null)
        {
            enemyAIController = GetComponent<EnemyAIController>();
        }
    }


    void Update()
    {
        // テスト用
        if (Input.GetMouseButtonDown(0))
        {
            SwordAttack();

        }

        // 攻撃コマンドの入力回数をカウントする処理
        if (isAttack)
        {
            weaponCollider.enabled = true;
        }
    }

    /// <summary>
    /// 剣での攻撃
    /// </summary>
    private void SwordAttack()
    {
        isAttack = true;
        weaponCollider.enabled = true;
        enemyAIController.agent.isStopped = true;

        Vector3 defaultAngle = new Vector3(rotateAngle.x, rotateAngle.y, rotateAngle.z);


        // Tween使います
        Sequence sequence = DOTween.Sequence();

        Debug.Log("攻撃開始");
        



            weaponCollider.enabled = false;
            isAttack = false;
            enemyAIController.agent.isStopped = false;
        
    }

}
