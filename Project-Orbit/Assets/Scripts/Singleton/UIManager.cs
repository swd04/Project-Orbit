using UnityEngine;

/// <summary>
/// 
/// </summary>
public class UIManager : SingletonBehaviour<UIManager>
{
    [Header("")]
    [SerializeField] private PlayerUIController playerUI = null;

    //
    protected override bool UseDontDestroy => false;

    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        if (!IsValidInstance) return;
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdatePlayerHP(float current, float max,bool isDamaged)
    {
        //
        if (playerUI == null)
        {
            Debug.LogWarning("PlayerUIControllerÇ™ê›íËÇ≥ÇÍÇƒÇ‹ÇπÇÒ");

            return;
        }

        //
        playerUI.UpdateHP(current, max);

        if (isDamaged)
        {
            playerUI.PlayDamageEffect();
        }
    }
}