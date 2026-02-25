using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ダメージ遅延バーを制御するクラス
/// </summary>
public class DamageDelayHPBar : HPBarBase
{
    [Header("ダメージ遅延用スライダー")]
    //メインHPバーの背面に表示される遅延バー
    [SerializeField] private Slider delaySlider = null;

    [Header("遅延時間")]
    //ダメージを受けてから遅延バーが動き始めるまでの待ち時間
    [SerializeField] private float delayDuration = 0.5f;

    [Header("追従速度")]
    //遅延バーがメインHPバーに追いつく速度
    [SerializeField] private float catchUpSpeed = 1f;

    [Header("遅延バーの色")]
    [SerializeField] private Color delayColor = new Color(1f, 0.5f, 0f);

    //遅延バーが最終的に目指すHP割合
    private float targetRatio = 0.0f;

    //遅延処理用タイマー
    private float delayTimer = 0.0f;

    //遅延バーのImage
    private Image delayFillImage = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //HPBarBaseの初期化処理を先に実行
        base.Awake();

        //遅延バーの初期設定
        if (delaySlider != null)
        {
            delaySlider.minValue = 0f;
            delaySlider.maxValue = 1f;
            delaySlider.value = 1f;

            //初期状態ではメインHPバーと同じ値にする
            delaySlider.value = hpSlider.value;
            targetRatio = hpSlider.value;
        }

        //遅延バーのImageを取得して色を設定
        if (delaySlider.fillRect != null)
        {
            delayFillImage = delaySlider.fillRect.GetComponent<Image>();
            delayFillImage.color = delayColor;
        }
    }

    /// <summary>
    /// HPが更新されたときに呼ばれる処理
    /// </summary>
    public override void UpdateHP(float current, float max)
    {
        //最大HPが不正な場合は何もしない
        if (max <= 0f) return;

        //新しいHP割合
        float newRatio = Mathf.Clamp01(current / max);

        //更新前のHP割合
        float prevRatio = hpSlider.value;

        //メインHPバーの更新処理
        base.UpdateHP(current, max);

        //HPが減少した場合
        if (newRatio < prevRatio)
        {
            //遅延バーの目標値を設定
            targetRatio = newRatio;

            //遅延時間をリセット
            delayTimer = delayDuration;
        }
        //HPが回復または同値の場合
        else
        {
            //遅延バーも即座に追従させる
            delaySlider.value = newRatio;
            targetRatio = newRatio;
            delayTimer = 0f;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        //遅延バーが設定されていなければ処理しない
        if (delaySlider == null) return;

        //すでに目標値以下なら何もしない
        if (delaySlider.value <= targetRatio) return;

        //遅延時間中は待機
        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        //現在の遅延バーの値から目標値へ徐々に近づける
        float newVal = Mathf.MoveTowards(
        delaySlider.value,
        targetRatio,
        catchUpSpeed * Time.deltaTime
        );

        //メインHPバーより前に行かないよう制限
        delaySlider.value = Mathf.Max(newVal, hpSlider.value);
    }

    /// <summary>
    /// 見た目更新処理
    /// </summary>
    protected override void UpdateVisual(float ratio) { }
}