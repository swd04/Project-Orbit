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

    /// <summary>
    /// フェードアウト制御用
    /// </summary>
    private CanvasGroup canvasGroup = null;

    /// <summary>
    /// 経過時間
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //CanvasGroup取得
        canvasGroup = GetComponent<CanvasGroup>();
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