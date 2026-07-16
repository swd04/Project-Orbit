using UnityEngine;

/// <summary>
/// 剣でプレイヤーを攻撃する処理を行うクラス
/// </summary>
public class SkeletonEnemyController : MonoBehaviour
{
    [Header("初期の角度(X軸)")]
    [SerializeField] private float initialAngle = 0.0f;

    [Header("武器の回転角度")]
    [SerializeField] private float rotateAngle = 0.0f;

    [Header("攻撃にかけるフレーム数(60FPS基準)")]
    [SerializeField] private int attackFrames = 0;

    [Header("武器のオブジェクト")]
    [SerializeField] private GameObject weaponObject = null;

    [Header("武器のコライダー")]
    [SerializeField] private Collider weaponCollider = null;

    [Header("EnemyAIController")]
    [SerializeField] private EnemyAIController enemyAIController = null;

    [Header("プレイヤーのどれくらい前で止まるか")]
    [SerializeField] private float stoppingDistance = 0.0f;

    [Header("攻撃中かどうか")]
    [SerializeField] private bool isAttack = false;

    [Header("回転中かどうか")]
    [SerializeField] private bool isRotating = false;

    [Header("元に戻す処理中かどうか")]
    [SerializeField] private bool isReturning = false;

    private Quaternion initialRotation = Quaternion.identity;
    private Quaternion targetRotation = Quaternion.identity;

    [Header("回転速度")]
    [SerializeField] private float rotateSpeed = 0.0f;

    private void Start()
    {
        if (weaponObject == null)
        {
            Debug.LogError("WeaponObjectが設定されていません。");
            return;
        }

        if (weaponCollider == null)
        {
            Debug.LogError("WeaponColliderが設定されていません。");
            return;
        }

        if (enemyAIController == null)
        {
            enemyAIController = GetComponent<EnemyAIController>();
        }

        initialRotation = weaponObject.transform.localRotation;
        initialAngle = weaponObject.transform.localEulerAngles.x;

        weaponCollider.enabled = false;
    }

    private void Update()
    {
        //// テスト用
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SwordAttack();
        //}

        if (enemyAIController.isAttack)
        {
            SwordAttack();
        }

        // 攻撃
        if (isRotating)
        {
            weaponObject.transform.localRotation = Quaternion.RotateTowards(weaponObject.transform.localRotation, targetRotation, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(weaponObject.transform.localRotation, targetRotation) < 0.1f)
            {
                isRotating = false;
                isReturning = true;
            }
        }

        // 元へ戻す
        if (isReturning)
        {
            weaponObject.transform.localRotation =
                Quaternion.RotateTowards(weaponObject.transform.localRotation, initialRotation, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(weaponObject.transform.localRotation, initialRotation) < 0.1f)
            {
                weaponObject.transform.localRotation = initialRotation;

                isReturning = false;
                isAttack = false;

                weaponCollider.enabled = false;
                enemyAIController.agent.isStopped = false;
            }
        }
    }

    /// <summary>
    /// 剣攻撃開始
    /// </summary>
    public void SwordAttack()
    {
        if (isRotating || isReturning)
        {
            return;
        }

        isAttack = true;

        weaponCollider.enabled = true;

        enemyAIController.agent.isStopped = true;
        enemyAIController.agent.stoppingDistance = stoppingDistance;

        // 現在の回転を保存
        initialRotation = weaponObject.transform.localRotation;

        // X軸のみ回転
        Vector3 euler = initialRotation.eulerAngles;

        targetRotation = Quaternion.Euler(initialAngle + rotateAngle, euler.y, euler.z);

        // フレーム数から1秒あたりの回転速度を計算
        rotateSpeed = rotateAngle / (attackFrames / 60f);

        isRotating = true;
    }
}