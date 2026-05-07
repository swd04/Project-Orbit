using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerUIController : MonoBehaviour
{
    [Header("")]
    [SerializeField] private PlayerHPBarUI playerHpBar = null;

    [Header("")]
    [SerializeField] private HPValueTextUI hpValueTextUI = null;

    [Header("")]
    [SerializeField] private DamageFlashUI damageFlashUI = null;

    /// <summary>
    /// 
    /// </summary>
    public void UpdateHP(float current, float max)
    {
        //
        if (playerHpBar != null)
        {
            playerHpBar.UpdateHP(current, max);
        }

        //
        if (hpValueTextUI != null)
        {
            hpValueTextUI.UpdateHP(current, max);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayDamageEffect()
    {
        //
        if (damageFlashUI != null)
        {
            damageFlashUI.PlayDamageFlash();
        }
    }
}