using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵用HPバークラス
/// </summary>
public class EnemyHPBarUI : DelaySliderHPBarBase
{
    [Header("HPバー設定")]

    //通常HPバーImage
    private Image mainFillImage = null;

    //通常HPバーの色
    [SerializeField] private Color mainHPColor = Color.red;

    [Header("遅延バー設定")]

    //遅延HPバーImage
    private Image delayFillImage = null;

    //遅延HPバーの色
    [SerializeField] private Color delayHPColor = new Color(1f, 0.5f, 0f);

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //親クラス初期化
        base.Awake();

        //メインHPバーImage取得
        if (hpSlider != null && hpSlider.fillRect != null)
        {
            mainFillImage = hpSlider.fillRect.GetComponent<Image>();
        }

        //遅延HPバーImage取得
        if (delaySlider != null && delaySlider.fillRect != null)
        {
            delayFillImage = delaySlider.fillRect.GetComponent<Image>();
        }

        //色設定
        if (mainFillImage != null)
        {
            mainFillImage.color = mainHPColor;
        }

        if (delayFillImage != null)
        {
            delayFillImage.color = delayHPColor;
        }
    }
}