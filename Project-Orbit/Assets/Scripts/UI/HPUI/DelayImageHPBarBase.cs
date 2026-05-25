using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Imageベースのダメージ遅延HPバー基底クラス
/// </summary>
public abstract class DelayImageHPBarBase : ImageHPBarBase
{
    [Header("遅延表示用Image")]
    [SerializeField] protected Image delayImage = null;

    [Header("遅延開始時間")]
    //ダメージを受けてから遅延バーが動くまでの待機時間
    [SerializeField] protected float delayDuration = 0.5f;

    [Header("追従速度")]
    //遅延バーが追いつく速度
    [SerializeField] protected float catchUpSpeed = 3f;

    //遅延バーが最終的に目指すHP割合
    protected float targetRatio = 1f;

    //遅延開始までのタイマー
    protected float delayTimer = 0f;

    //前フレームのHP割合
    private float currentRatio;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected virtual void Awake()
    {
        //メインHPバー初期化
        if (hpImage != null)
        {
            hpImage.fillAmount = 1f;
        }

        //遅延バー初期化
        if (delayImage != null)
        {
            delayImage.fillAmount = hpImage != null ? hpImage.fillAmount : 1f;
        }

        //初期HP状態
        targetRatio = hpImage != null ? hpImage.fillAmount : 1f;
        currentRatio = targetRatio;
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Start()
    {
        //
        if (hpImage == null || delayImage == null) return;

        //
        delayImage.fillAmount = hpImage.fillAmount;
    }

    /// <summary>
    /// HP表示更新処理
    /// </summary>
    protected override void UpdateVisual(float ratio)
    {
        if (hpImage == null) return;

        //前回HPとの差分検知用
        float prevRatio = currentRatio;
        currentRatio = ratio;

        //基底クラスでHPバー更新
        base.UpdateVisual(ratio);

        //HP減少時：遅延バーを遅らせる
        if (ratio < prevRatio)
        {
            targetRatio = ratio;
            delayTimer = delayDuration;
        }
        //HP回復時：遅延バーは即追従
        else
        {
            targetRatio = ratio;

            if (delayImage != null)
            {
                delayImage.fillAmount = GetVisualRatio(ratio);
                delayTimer = 0f;
            }
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected virtual void Update()
    {
        if (delayImage == null || hpImage == null) return;

        //待機時間中は何もしない
        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;

            return;
        }

        //目標HP
        float visualTarget = GetVisualRatio(targetRatio);

        //徐々に追従
        delayImage.fillAmount = Mathf.MoveTowards(
            delayImage.fillAmount,
            visualTarget,
            catchUpSpeed * Time.deltaTime
        );
    }
}