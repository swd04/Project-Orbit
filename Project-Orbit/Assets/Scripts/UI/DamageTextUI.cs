using TMPro;
using UnityEngine;

/// <summary>
/// ダメージ数値表示UI
/// </summary>
public class DamageTextUI : MonoBehaviour
{
    [Header("ダメージ表示テキスト")]
    [SerializeField] private TMP_Text damageText = null;

    [Header("上方向への移動速度")]
    [SerializeField] private float moveSpeed = 50f;

    [Header("表示時間")]
    [SerializeField] private float lifeTime = 1f;

    [Header("拡大演出時間")]
    [SerializeField] private float popDuration = 0.5f;

    [Header("開始サイズ")]
    [SerializeField] private Vector3 startScale = Vector3.one * 10.0f;

    /// <summary>
    /// フェードアウト制御用
    /// </summary>
    private CanvasGroup canvasGroup = null;

    /// <summary>
    /// 経過時間
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 終了サイズ
    /// </summary>
    private readonly Vector3 endScale = Vector3.one;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //CanvasGroup取得
        canvasGroup = GetComponent<CanvasGroup>();

        //最初は大きめ
        transform.localScale = startScale;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //上方向へ移動
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        //経過時間加算
        timer += Time.deltaTime;

        //最初だけ縮小演出
        if (timer < popDuration)
        {
            float t = timer / popDuration;

            transform.localScale =
                Vector3.Lerp(
                    startScale,
                    endScale,
                    t);
        }
        else
        {
            transform.localScale = endScale;
        }

        //徐々に透明化
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f - (timer / lifeTime);
        }

        //表示時間経過で削除
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ダメージ値設定
    /// </summary>
    public void SetDamage(int damage)
    {
        //ダメージ値表示
        damageText.text = damage.ToString();
    }
}