using UnityEngine;

/// <summary>
/// プレイヤー用HPバーUIクラス
/// </summary>
public class PlayerHPBarUI : DamageDelayHPBar
{
    [Header("HPバーの色設定")]

    //100%～50％
    [SerializeField] private Color highHPColor = Color.green;

    //50％～25％
    [SerializeField] private Color midHPColor = Color.yellow;

    //25％以降
    [SerializeField] private Color lowHPColor = Color.red;

    /// <summary>
    /// HP割合に応じてHPバーの色を更新する処理
    /// </summary>
    protected override void UpdateVisual(float ratio)
    {
        //HPが半分以上ある場合
        if (ratio > 0.5f)
        {
            hpFillImage.color = highHPColor;
        }
        //HPが残り少なくなってきた場合
        else if (ratio > 0.25f)
        {
            hpFillImage.color = midHPColor;
        }
        //瀕死状態
        else
        {
            hpFillImage.color = lowHPColor;
        }
    }
}