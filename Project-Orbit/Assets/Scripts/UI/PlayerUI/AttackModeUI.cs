using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 攻撃モードの状態をUIに表示するクラス
/// </summary>
public class AttackModeUI : MonoBehaviour
{
    [Header("攻撃モードの状態を管理しているモデル")]
    [SerializeField] private PlayerChoseAttackMode modeModel = null;

    [Header("捕食UI")]
    [SerializeField] private Image predationImage = null;

    [Header("魂化UI")]
    [SerializeField] private Image soulReinforceImage = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //起動時の攻撃モードをUIに反映
        Refresh(modeModel.CurrentAttackMode);

        //攻撃モード変更イベントを購読
        modeModel.OnAttackModeChanged += Refresh;
    }

    /// <summary>
    /// 破棄時の後処理
    /// </summary>
    private void OnDestroy()
    {
        //nullチェックして安全に解除
        if (modeModel != null)
        {
            modeModel.OnAttackModeChanged -= Refresh;
        }
    }

    /// <summary>
    /// 攻撃モードに応じてUI表示を切り替える処理
    /// </summary>
    private void Refresh(PlayerChoseAttackMode.AttackMode mode)
    {
        //現在のモード判定
        bool isPredation = mode == PlayerChoseAttackMode.AttackMode.PREDATION;
        bool isSoul = mode == PlayerChoseAttackMode.AttackMode.SOULREINFORCE;

        //捕食モードUIの表示切り替え
        if (predationImage != null)
        {
            predationImage.enabled = isPredation;
        }

        //魂化モードUIの表示切り替え
        if (soulReinforceImage != null)
        {
            soulReinforceImage.enabled = isSoul;
        }
    }
}