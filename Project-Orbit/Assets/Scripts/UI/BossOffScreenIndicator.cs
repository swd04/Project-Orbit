using UnityEngine;

/// <summary>
/// 画面外にいるボスの方向を表示するクラス
/// </summary>
public class BossOffScreenIndicator : MonoBehaviour
{
    [Header("ボス")]
    [SerializeField] private Transform boss = null;

    [Header("アイコン")]
    [SerializeField] private RectTransform icon = null;

    [Header("メインカメラ")]
    [SerializeField] private Camera targetCamera = null;

    [Header("画面端からの距離")]
    [SerializeField] private float screenMargin = 50f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //カメラが設定されていない場合はメインカメラを取得する
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //ボス・カメラ・アイコンのいずれかが存在しない場合はアイコンを非表示にする
        if (boss == null || !boss.gameObject.activeInHierarchy || targetCamera == null || icon == null)
        {
            //アイコンが存在する場合のみ非表示にする
            if (icon != null)
            {
                icon.gameObject.SetActive(false);
            }

            return;
        }

        //ボスのワールド座標をビューポート座標に変換する
        //ビューポート座標は画面左下が(0,0)、右上が(1,1)になる
        Vector3 viewPosition = targetCamera.WorldToViewportPoint(boss.position);

        //ボスがカメラの前方かつ画面内にいるか判定する
        bool isOnScreen =
            viewPosition.z > 0f &&
            viewPosition.x >= 0f &&
            viewPosition.x <= 1f &&
            viewPosition.y >= 0f &&
            viewPosition.y <= 1f;

        //ボスが画面外の場合のみアイコンを表示する
        icon.gameObject.SetActive(!isOnScreen);

        //ボスが画面内にいる場合はここで処理を終了する
        if (isOnScreen)
        {
            return;
        }

        //画面中央からボスがいる方向へのベクトルを求める
        Vector2 direction = new Vector2(viewPosition.x - 0.5f, viewPosition.y - 0.5f);

        //ボスがカメラの後方にいる場合は方向を反転させる
        if (viewPosition.z < 0f)
        {
            direction *= -1f;
        }

        //方向ベクトルを正規化して長さを1にする
        direction.Normalize();

        //画面中央のスクリーン座標を取得する
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        //画面中央からボスの方向へ大きく移動させる
        Vector2 screenPosition = screenCenter + direction * Mathf.Max(Screen.width, Screen.height);

        //アイコンが画面の外に出ないようにX座標を制限する
        screenPosition.x = Mathf.Clamp(screenPosition.x, screenMargin, Screen.width - screenMargin);

        //アイコンが画面の外に出ないようにY座標を制限する
        screenPosition.y = Mathf.Clamp(screenPosition.y, screenMargin, Screen.height - screenMargin);

        //計算したスクリーン座標にアイコンを配置する
        icon.position = screenPosition;
    }
}