using UnityEngine;

/// <summary>
/// カメラとの距離に応じてUIサイズを補正するクラス
/// </summary>
public class DistanceScale : MonoBehaviour
{
    [Header("参照カメラ")]
    [SerializeField] private Camera cam = null;

    [Header("スケール設定")]

    [Header("距離に応じて拡大・縮小する倍率")]
    [SerializeField] private float baseScale = 0.2f;

    [Header("最小スケール値")]
    [SerializeField] private float minScale = 0.5f;

    [Header("最大スケール値")]
    [SerializeField] private float maxScale = 1.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Awake()
    {
        //カメラが未設定ならMainCameraを取得
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    /// <summary>
    /// 距離に応じてスケールを更新する処理
    /// </summary>
    void LateUpdate()
    {
        //カメラが存在しない場合は処理しない
        if (cam == null) return;

        //カメラとの距離を取得
        float dist = Vector3.Distance(cam.transform.position, transform.position);

        //距離に応じたスケール値を計算
        float scale = baseScale * dist;

        //最小・最大サイズで制限
        scale = Mathf.Clamp(scale, minScale, maxScale);

        //スケールを反映
        transform.localScale = Vector3.one * scale;
    }
}