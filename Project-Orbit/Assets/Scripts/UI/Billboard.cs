using UnityEngine;

/// <summary>
/// カメラの方向を向かせるクラス
/// </summary>
public class Billboard : MonoBehaviour
{
    [Header("参照カメラ")]
    [SerializeField] private Camera cam = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //カメラが未設定ならMainCameraを取得
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    /// <summary>
    /// カメラ方向へ回転する処理
    /// </summary>
    private void LateUpdate()
    {
        //カメラが存在しない場合は処理しない
        if (cam == null) return;

        //カメラ方向を取得
        Vector3 dir = cam.transform.position - transform.position;

        //Y軸回転のみ使用するため上下成分を無効化
        dir.y *= 0.2f;

        //回転方向が0の場合は処理しない
        if (dir == Vector3.zero) return;

        //カメラ方向へ回転
        transform.rotation = Quaternion.LookRotation(-dir);
    }
}