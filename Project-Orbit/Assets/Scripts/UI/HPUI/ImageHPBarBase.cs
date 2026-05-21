using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ImageベースのHPバー基底クラス
/// </summary>
public abstract class ImageHPBarBase : HPUIBase
{
    [Header("HP表示用Image")]
    [SerializeField] protected Image hpImage = null;

    /// <summary>
    /// HP表示更新処理
    /// </summary>
    protected override void UpdateVisual(float ratio)
    {
        //Image未設定時の事故防止
        if (hpImage == null) return;

        //見た目用割合取得
        float visualRatio = GetVisualRatio(ratio);

        //HP割合をImageへ反映
        hpImage.fillAmount = visualRatio;

        //派生クラス固有の演出処理
        OnUpdateVisual(ratio);
    }

    /// <summary>
    /// 派生クラス専用の見た目更新処理
    /// </summary>
    protected virtual void OnUpdateVisual(float ratio) { }

    /// <summary>
    /// 見た目用割合取得
    /// </summary>
    protected virtual float GetVisualRatio(float ratio)
    {
        return ratio;
    }
}