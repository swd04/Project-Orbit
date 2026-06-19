using TMPro;
using UnityEngine;

/// <summary>
/// プレイヤーステータス表示UI
/// </summary>
public class MenuPlayerStatusUI : MonoBehaviour
{
    [Header("プレイヤーステータス")]
    [SerializeField] private PlayerStatus playerStatus = null;

    [Header("プレイヤーHPText")]
    [SerializeField] private TMP_Text hpText = null;

    [Header("プレイヤー攻撃力Text")]
    [SerializeField] private TMP_Text attackText = null;

    [Header("プレイヤー防御力Text")]
    [SerializeField] private TMP_Text defenceText = null;

    /// <summary>
    /// ステータス表示処理
    /// </summary>
    public void Refresh()
    {
        //HP表示
        hpText.text = $"HP {playerStatus.Life}";

        //攻撃力表示
        attackText.text = $"攻撃力 {playerStatus.Attack}";

        //防御力表示
        defenceText.text = $"防御力 {playerStatus.Defence}";
    }

    /// <summary>
    /// UI有効化時処理
    /// </summary>
    private void OnEnable()
    {
        //最新ステータスを反映
        Refresh();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //ステータスを毎フレーム更新
        Refresh();
    }
}