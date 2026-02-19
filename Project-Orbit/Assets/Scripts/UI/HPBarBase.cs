using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPバーUIの共通基底クラス
/// </summary>
public abstract class HPBarBase : MonoBehaviour
{
    [Header("HPバーの塗り部分")]
    [SerializeField] protected Slider hpSlider = null;

    //SliderのFill部分に付いているImage
    //見た目制御で使用
    protected Image hpFillImage;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected virtual void Awake()
    {
        //Sliderが未設定の場合の事故防止
        if (hpSlider != null)
        {
            //HPは割合で扱うため0〜1に固定
            hpSlider.minValue = 0f;
            hpSlider.maxValue = 1f;
        }

        //SliderのFill部分からImageを取得
        if (hpSlider.fillRect != null)
        {
            hpFillImage = hpSlider.fillRect.GetComponent<Image>();
        }
    }

    /// <summary>
    /// HPバーを更新する処理
    /// 
    /// 【役割】
    /// HPの絶対値ではなく割合でUIを制御
    /// 更新ルールは全UI共通
    /// 色変更や演出などの見た目処理はUpdateVisualに任せる
    /// </summary>
    public void UpdateHP(float current, float max)
    {
        //Image未設定時の事故防止
        if (hpSlider == null) return;

        //HP割合に正規化
        float ratio = Mathf.Clamp01(current / max);

        //HPバーの長さ更新
        hpSlider.value = ratio;

        //見た目を派生クラスで更新
        UpdateVisual(ratio);
    }

    /// <summary>
    /// HP割合に応じた見た目更新処理
    /// 
    ///【役割】
    /// 色の切り替え
    /// 点滅
    /// アニメーション
    /// 警告演出など
    /// </summary>
    protected abstract void UpdateVisual(float ratio);
}