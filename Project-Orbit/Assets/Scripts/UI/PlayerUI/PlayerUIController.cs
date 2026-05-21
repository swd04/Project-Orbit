using UnityEngine;

/// <summary>
/// プレイヤーUI制御クラス
/// </summary>
public class PlayerUIController : MonoBehaviour
{
    [Header("プレイヤーHPバー")]
    [SerializeField] private PlayerHPBarUI playerHpBar = null;

    [Header("HP数値表示")]
    [SerializeField] private HPValueTextUI hpValueTextUI = null;

    [Header("ダメージ演出")]
    [SerializeField] private DamageFlashUI damageFlashUI = null;

    //前回HP保持
    private float previousHP = -1f;

    /// <summary>
    /// HP更新処理
    /// </summary>
    public void UpdateHP(float current, float max)
    {
        //ダメージ判定
        bool isDamaged = previousHP >= 0f && current < previousHP;

        //HPバー更新
        if (playerHpBar != null)
        {
            playerHpBar.UpdateHP(current, max);
        }

        //数値更新
        if (hpValueTextUI != null)
        {
            hpValueTextUI.UpdateHP(current, max);
        }

        //被ダメ演出
        if (isDamaged)
        {
            PlayDamageEffect();
        }

        //現在HP保存
        previousHP = current;
    }

    /// <summary>
    /// ダメージ演出再生
    /// </summary>
    public void PlayDamageEffect()
    {
        if (damageFlashUI != null)
        {
            damageFlashUI.PlayDamageFlash();
        }
    }
}