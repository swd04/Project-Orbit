using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵用HPバークラス
/// </summary>
public class EnemyHPBarUI : DelaySliderHPBarBase
{
    [Header("HPバー設定")]

    //通常HPバーImage
    private Image mainFillImage = null;

    //通常HPバーの色
    [SerializeField] private Color mainHPColor = Color.red;

    [Header("遅延バー設定")]

    //遅延HPバーImage
    private Image delayFillImage = null;

    //遅延HPバーの色
    [SerializeField] private Color delayHPColor = new Color(1f, 0.5f, 0f);

    [Header("CanvasGroup取得")]
    [SerializeField] private CanvasGroup canvasGroup = null;

    [Header("HPバー非表示までの時間")]
    [SerializeField] private float hideTime = 10f;

    /// <summary>
    /// HPバー非表示用タイマー
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //親クラス初期化
        base.Awake();

        //メインHPバーImage取得
        if (hpSlider != null && hpSlider.fillRect != null)
        {
            mainFillImage = hpSlider.fillRect.GetComponent<Image>();
        }

        //遅延HPバーImage取得
        if (delaySlider != null && delaySlider.fillRect != null)
        {
            delayFillImage = delaySlider.fillRect.GetComponent<Image>();
        }

        //メインHPバー色設定
        if (mainFillImage != null)
        {
            mainFillImage.color = mainHPColor;
        }

        //遅延HPバー色設定
        if (delayFillImage != null)
        {
            delayFillImage.color = delayHPColor;
        }
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        HideHPBar();
    }

    /// <summary>
    /// HPバーを初期化するメソッド
    /// </summary>
    public void Initialize(int maxHp)
    {
        //スライダーの最大値設定
        hpSlider.maxValue = maxHp;
        delaySlider.maxValue = maxHp;

        //初期値を最大HPに設定
        hpSlider.value = maxHp;
        delaySlider.value = maxHp;
    }

    /// <summary>
    /// HPバーを更新するメソッド
    /// </summary>
    public void UpdateHP(int currentHp)
    {
        //HPバー表示
        ShowHPBar();

        //通常HPバーを即時更新
        hpSlider.value = currentHp;

        //既存コルーチン停止
        StopAllCoroutines();

        //遅延HPバー更新開始
        StartCoroutine(UpdateDelayHP(currentHp));
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //タイマーがある時
        if (timer > 0)
        {
            //時間減少
            timer -= Time.deltaTime;

            //時間切れ
            if (timer <= 0)
            {
                HideHPBar();
            }
        }
    }

    /// <summary>
    /// HPバー表示処理
    /// </summary>
    public void ShowHPBar()
    {
        //表示
        canvasGroup.alpha = 1f;

        //UI操作有効
        canvasGroup.interactable = true;

        //Raycast有効
        canvasGroup.blocksRaycasts = true;

        //非表示タイマーリセット
        timer = hideTime;
    }

    /// <summary>
    /// HPバー非表示処理
    /// </summary>
    private void HideHPBar()
    {
        //非表示
        canvasGroup.alpha = 0f;

        //UI操作無効
        canvasGroup.interactable = false;

        //Raycast無効
        canvasGroup.blocksRaycasts = false;

        //タイマー初期化
        timer = 0f;
    }

    /// <summary>
    /// 遅延HPバーを滑らかに減少させるコルーチン
    /// </summary>
    private System.Collections.IEnumerator UpdateDelayHP(int targetHp)
    {
        //少し遅延させる
        yield return new WaitForSeconds(0.2f);

        //現在値が目標値より大きい間更新
        while (delaySlider.value != targetHp)
        {
            //徐々にHP更新
            delaySlider.value = Mathf.MoveTowards(
                delaySlider.value,
                targetHp,
                Time.deltaTime * 100f);

            yield return null;
        }

        //最終的に目標値へ合わせる
        delaySlider.value = targetHp;
    }
}