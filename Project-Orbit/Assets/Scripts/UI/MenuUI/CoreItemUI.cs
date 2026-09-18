using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// コア一覧1行分の表示UI
/// </summary>
public class CoreItemUI : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("コア名表示Text")]
    [SerializeField] private TMP_Text coreNameText = null;

    [Header("レベル表示Text")]
    [SerializeField] private TMP_Text countText = null;

    [Header("レベルアップゲージ")]
    [SerializeField] private Slider levelUpSlider = null;

    [Header("取得数表示Text")]
    [SerializeField] private TMP_Text levelUpCountText = null;

    /// <summary>
    /// このUIが表すコア
    /// </summary>
    private SoulCore soulCore;

    /// <summary>
    /// このUIを管理しているコア一覧UI
    /// </summary>
    private MenuCoreCollectionUI coreListUI;

    /// <summary>
    /// コア一覧UIを設定する
    /// </summary>
    public void Initialize(MenuCoreCollectionUI listUI)
    {
        coreListUI = listUI;
    }

    /// <summary>
    /// コア情報表示処理
    /// </summary>
    public void SetData(SoulCore core)
    {
        //コア情報保存
        soulCore = core;

        //コア名表示
        coreNameText.text = core.coreName;

        //所持数表示
        countText.text = $"Lv.{core.soulLevel}";

        //レベルアップゲージ更新
        UpdateLevelUpGauge();
    }

    /// <summary>
    /// レベルアップゲージと次のレベルまでの取得数を更新処理
    /// </summary>
    private void UpdateLevelUpGauge()
    {
        //表示するコアが設定されていなければ処理を終了
        if (soulCore == null)
        {
            return;
        }

        //現在のレベルでレベルアップに必要な取得数を取得
        int requiredCount = soulCore.RequiredCoreCount;

        //現在のレベルで取得しているコア数を取得
        int currentCount = soulCore.CoreGetCount;

        //必要取得数が0以下なら最大レベルとして扱う
        if (requiredCount <= 0)
        {
            //レベルアップゲージを満タンにする
            if (levelUpSlider != null)
            {
                levelUpSlider.value = 1.0f;
            }

            //最大レベルであることを表示
            if (levelUpCountText != null)
            {
                levelUpCountText.text = "Max";
            }

            return;
        }

        //レベルアップゲージを更新
        if (levelUpSlider != null)
        {
            //ゲージの最大値を次のレベルに必要な取得数に設定
            levelUpSlider.maxValue = requiredCount;

            //現在の取得数をゲージに反映
            levelUpSlider.value = currentCount;
        }

        //次のレベルまでの残り取得数を表示
        if (levelUpCountText != null)
        {
            //必要取得数から現在の取得数を引いて残りを計算
            int nextCount = requiredCount - currentCount;

            //次のレベルまでの残り取得数を表示
            levelUpCountText.text = $"NEXT {nextCount}";
        }
    }

    /// <summary>
    /// このUIが保持しているコアを取得する処理
    /// </summary>
    public SoulCore GetSoulCore()
    {
        return soulCore;
    }

    /// <summary>
    /// コアを左クリックした時の処理
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        //左クリック以外は無視
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        //クリックされたコアを装備する
        coreListUI.OnClickItem(soulCore);
    }

    /// <summary>
    /// コアにカーソルを乗せた時の処理
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //コアが設定されていなければ処理を終了
        if (soulCore == null)
        {
            return;
        }

        //スキル説明を表示
        coreListUI.ShowSkillDescription(soulCore,transform.position);
    }

    /// <summary>
    /// コアからカーソルが離れた時の処理
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        //スキル説明を非表示
        coreListUI.HideSkillDescription();
    }
}