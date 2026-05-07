using UnityEngine;
using TMPro;

/// <summary>
/// 攻撃モードの状態をUIに表示するクラス
/// </summary>
public class AttackModeUI : MonoBehaviour
{
    [Header("攻撃モードの状態を管理しているモデル")]
    [SerializeField] private PlayerChoseAttackMode modeModel = null;

    [Header("攻撃モードを表示するテキスト")]
    [SerializeField] private TextMeshProUGUI modeText = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //起動時点の攻撃モードをUIに反映
        Refresh(modeModel.CurrentAttackMode);

        //攻撃モードが変更されたらRefreshが呼ばれるようにする
        modeModel.OnAttackModeChanged += Refresh;
    }

    /// <summary>
    /// 破棄時の後処理
    /// </summary>
    private void OnDestroy()
    {
        //イベント購読解除
        modeModel.OnAttackModeChanged -= Refresh;
    }

    /// <summary>
    /// 攻撃モードに応じてUI表示を更新する処理
    /// </summary>
    private void Refresh(PlayerChoseAttackMode.AttackMode mode)
    {
        //攻撃モードに応じて表示テキストを切り替える
        modeText.text = mode switch
        {
            PlayerChoseAttackMode.AttackMode.PREDATION => "PREDATION",
            PlayerChoseAttackMode.AttackMode.SOULREINFORCE => "SOULREINFORCE",
            _ => ""
        };
    }
}