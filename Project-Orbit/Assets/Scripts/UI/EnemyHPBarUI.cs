using UnityEngine;

/// <summary>
/// 敵用HPバークラス
/// </summary>
public class EnemyHPBarUI : HPBarBase
{
    [Header("HPバーの色設定")]

    //HPバーの固定色
    [SerializeField] private Color fixedHPColor = Color.red;

    /// <summary>
    /// HPバーの見た目の更新処理
    /// </summary>
    protected override void UpdateVisual(float ratio)
    {
        //HP割合に関係なく常に同じ色
        hpFillImage.color = fixedHPColor;
    }
}