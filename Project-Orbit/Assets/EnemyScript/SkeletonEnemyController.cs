using UnityEngine;
using DG.Tweening;

/// <summary>
/// 剣でプレイヤーを攻撃する処理を行うクラス
/// </summary>
public class SkeletonEnemyController : MonoBehaviour
{
    // Aiの行動パターン選択と同期させる


    // アニメーションイベントで武器のコライダーを動かす
    [Header("武器を取得")]
    [SerializeField] private GameObject swordWeapon = null;

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

    void Update()
    {
        // テスト用
        if (Input.GetMouseButtonDown(0))
        {
            SwordAttack();
            isAttack = true;
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

}
