using UnityEngine;

/// <summary>
/// プレイヤーの動きを処理するクラス
/// </summary>
public class ControlPlayer : MonoBehaviour
{
    [Header("PlayerのRigidbodyの取得")]
    [SerializeField] private Rigidbody playerRigidbody = default;

    [Header("カメラのTransformを取得")]
    [SerializeField] private Transform cameraTransform = default;

    [Header("プレイヤーの通常速度")]
    [SerializeField] private float moveSpeed = 0.0f;

    [Header("プレイヤーのダッシュ速度")]
    [SerializeField] private float dashSpeed = 0.0f;

    [Header("プレイヤーの現在の速度")]
    [SerializeField] private float currentSpeed = 0.0f;

    [Header("プレイヤーのダッシュ判定を行うスクリプトの取得")]
    [SerializeField] private PlayerDash playerDash = default;


    private void Start()
    {
        currentSpeed = moveSpeed;
    }

    /// <summary>
    /// プレイヤーの移動処理を行うメソッド
    /// FixedUpdateは物理演算の更新に合わせて呼び出されるため、Rigidbodyを使用した移動処理に適する
    /// </summary>
    private void FixedUpdate()
    {
        // プレイヤー操作
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        // ダッシュ状態に応じて速度を切り替える
        if (playerDash.isDashPlayer)
        {
            currentSpeed = dashSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        // WASDによる移動処理
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // カメラのY軸の回転を取得
        Quaternion horizontalRotation = Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up);

        // カメラの回転を考慮した移動方向を計算
        Vector3 direction = horizontalRotation * input;

        if (direction.sqrMagnitude > 0.01f)
        {
            // プレイヤーの向きを移動方向に向る
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerRigidbody.MoveRotation(Quaternion.Slerp(playerRigidbody.rotation, targetRotation, Time.fixedDeltaTime * 10.0f));
        }

        // 移動方向の大きさが1を超える場合は正規化する
        if (direction.sqrMagnitude > 1.0f)
        {
            direction.Normalize();
        }

        // Rigidbodyを使用してプレイヤーを移動させる
        direction *= currentSpeed;

        // プレイヤーの移動方向をRigidbodyの速度に設定
        direction.y = playerRigidbody.linearVelocity.y;

        // Rigidbodyの速度を更新
        playerRigidbody.linearVelocity = direction;
    }
}
