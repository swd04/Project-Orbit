using UnityEngine;

/// <summary>
/// UI管理クラス
/// </summary>
public class UIManager : SingletonBehaviour<UIManager>
{
    [Header("プレイヤーUI")]
    [SerializeField] private PlayerUIController playerUI = null;

    //シーン移動時、破棄するか否か
    protected override bool UseDontDestroy => false;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //
        base.Awake();

        //
        if (!IsValidInstance) return;
    }

    /// <summary>
    /// プレイヤーHP更新処理
    /// </summary>
    public void UpdatePlayerHP(float current, float max)
    {
        //UI未設定時
        if (playerUI == null)
        {
            Debug.LogWarning("PlayerUIControllerが設定されてません");

            return;
        }

        //UI更新
        playerUI.UpdateHP(current, max);
    }
}