using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SliderベースのHPバー基底クラス
/// </summary>
public abstract class SliderHPBarBase : HPUIBase
{
    [Header("HP表示用Slider")]
    [SerializeField] protected Slider hpSlider = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected virtual void Awake()
    {
        //Slider未設定時の事故防止
        if (hpSlider != null)
        {
            //HP割合で扱うため0〜1に固定
            hpSlider.minValue = 0f;
            hpSlider.maxValue = 1f;
        }
    }

    /// <summary>
    /// HP表示更新処理
    /// </summary>
    protected override void UpdateVisual(float ratio)
    {
        //Slider未設定時の事故防止
        if (hpSlider == null) return;

        //HP割合をSliderへ反映
        hpSlider.value = ratio;

        //派生クラス固有の演出処理
        OnUpdateVisual(ratio);
    }

    /// <summary>
    /// 派生クラス専用の見た目更新処理
    /// </summary>
    protected virtual void OnUpdateVisual(float ratio) { }
}