using TMPro;
using UnityEngine;

/// <summary>
/// ボス専用HPバーUI
/// </summary>
public class BossHPBarUI : DelaySliderHPBarBase
{
    [Header("ボス名")]
    [SerializeField] private TMP_Text bossNameText = null;

    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroup = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        HideBoss();
    }

    /// <summary>
    /// ボス出現時処理
    /// </summary>
    public void ShowBoss(string bossName, int maxHp)
    {
        //UIオブジェクト有効化
        gameObject.SetActive(true);

        //ボス名表示
        bossNameText.text = bossName;

        //UI表示
        canvasGroup.alpha = 1f;

        //HP満タン状態で初期化
        UpdateHP(maxHp, maxHp);
    }

    /// <summary>
    /// ボスHP更新処理
    /// </summary>
    public void UpdateBossHP(int currentHp, int maxHp)
    {
        //HPバー更新
        UpdateHP(currentHp, maxHp);
    }

    /// <summary>
    /// ボス撃破時処理
    /// </summary>
    public void HideBoss()
    {
        //UI非表示
        canvasGroup.alpha = 0f;

        //ボス名クリア
        bossNameText.text = "";

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 見た目更新処理
    /// </summary>
    protected override void OnUpdateVisual(float ratio) { }
}