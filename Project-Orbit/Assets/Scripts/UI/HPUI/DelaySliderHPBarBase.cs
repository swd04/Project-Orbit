using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sliderベースのダメージ遅延HPバー基底クラス
/// </summary>
public abstract class DelaySliderHPBarBase : SliderHPBarBase
{
    [Header("遅延表示用Slider")]
    [SerializeField] protected Slider delaySlider = null;

    [Header("遅延開始時間")]
    //ダメージを受けてから遅延バーが動くまでの待機時間
    [SerializeField] protected float delayDuration = 0.5f;

    [Header("追従速度")]
    //遅延バーが追いつく速度
    [SerializeField] protected float catchUpSpeed = 3f;

    //遅延バーが最終的に目指すHP割合
    protected float targetRatio = 1f;

    //遅延開始までのタイマー
    protected float delayTimer = 0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //親クラス初期化
        base.Awake();

        //遅延バー初期化
        if (delaySlider != null)
        {
            delaySlider.minValue = 0f;
            delaySlider.maxValue = 1f;

            delaySlider.value = hpSlider != null ? hpSlider.value : 1f;
        }

        //初期目標値
        targetRatio = hpSlider != null ? hpSlider.value : 1f;
    }

    /// <summary>
    /// HP表示更新処理
    /// </summary>
    protected override void UpdateVisual(float ratio)
    {
        //現在のHP割合取得
        float prevRatio = hpSlider != null ? hpSlider.value : ratio;

        //通常HPバー更新
        base.UpdateVisual(ratio);

        //HP減少時
        if (ratio < prevRatio)
        {
            //遅延バー目標値更新
            targetRatio = ratio;

            //遅延タイマー開始
            delayTimer = delayDuration;
        }
        //HP回復時
        else
        {
            //遅延バー即時反映
            if (delaySlider != null)
            {
                delaySlider.value = ratio;
            }

            //目標値更新
            targetRatio = ratio;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected virtual void Update()
    {
        //Slider未設定時の事故防止
        if (delaySlider == null || hpSlider == null) return;

        //待機中
        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;

            return;
        }

        //遅延バー追従
        delaySlider.value = Mathf.MoveTowards(
            delaySlider.value,
            targetRatio,
            catchUpSpeed * Time.deltaTime
        );

        //メインHPバーより前に出ないよう制限
        delaySlider.value = Mathf.Max(
            delaySlider.value,
            hpSlider.value
        );
    }
}