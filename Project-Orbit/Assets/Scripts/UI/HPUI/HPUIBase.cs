using UnityEngine;

/// <summary>
/// HPバーUIの共通基底クラス
/// </summary>
public abstract class HPUIBase : MonoBehaviour
{
    /// <summary>
    /// HPバーを更新する処理
    /// 
    /// 【役割】
    /// HPの絶対値ではなく割合でUIを制御
    /// 更新ルールは全UI共通
    /// 色変更や演出などの見た目処理はUpdateVisualに任せる
    /// </summary>
    public virtual void UpdateHP(float current, float max)
    {
        //0除算防止
        if (max <= 0f) return;

        //HP割合に正規化
        float ratio = Mathf.Clamp01(current / max);

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