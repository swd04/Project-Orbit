using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤー専用HPバーUIクラス
/// </summary>
public class PlayerHPBarUI : DelayImageHPBarBase
{
    [Header("プレイヤーHPバーのHP割合に応じた色設定")]

    //HPが十分ある時の色
    [SerializeField] private Color highHPColor = Color.green;

    //HPが減ってきた時の色
    [SerializeField] private Color midHPColor = Color.yellow;

    //瀕死時の色
    [SerializeField] private Color lowHPColor = Color.red;

    [Header("遅延バー設定")]

    //遅延バーの色
    [SerializeField] private Color delayBarColor = new Color(1f, 0.5f, 0f);

    [Header("HP割合設定")]

    //中HPへ切り替わる割合
    [SerializeField] private float midHPThreshold = 0.5f;

    //低HPへ切り替わる割合
    [SerializeField] private float lowHPThreshold = 0.25f;

    [Header("プレイヤーステータス")]
    [SerializeField] private PlayerStatus playerStatus = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //親クラス初期化
        base.Awake();

        //遅延バー色設定
        if (delayImage != null)
        {
            delayImage.color = delayBarColor;
        }
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //HP変更通知を登録
        playerStatus.OnHPChanged += UpdateHPBar;


        //初期表示
        UpdateHPBar(
            playerStatus.Life,
            playerStatus.MaxHP
        );
    }

    /// <summary>
    /// HPバー更新処理
    /// </summary>
    private void UpdateHPBar(int hp, int maxHp)
    {
        float ratio = (float)hp / maxHp;

        UpdateHP(hp, maxHp);
    }

    /// <summary>
    /// 見た目更新処理
    /// </summary>
    protected override void OnUpdateVisual(float ratio)
    {
        //Image未設定時の事故防止
        if (hpImage == null) return;

        //HPが十分ある場合
        if (ratio > midHPThreshold)
        {
            hpImage.color = highHPColor;
        }
        //HPが減ってきた場合
        else if (ratio > lowHPThreshold)
        {
            hpImage.color = midHPColor;
        }
        //瀕死状態
        else
        {
            hpImage.color = lowHPColor;
        }
    }

    /// <summary>
    /// 見た目用HP割合取得
    /// </summary>
    protected override float GetVisualRatio(float ratio)
    {
        //HPはMAXでも少し欠ける
        return ratio * 0.8f;
    }

    private void OnDestroy()
    {
        if (playerStatus != null)
        {
            playerStatus.OnHPChanged -= UpdateHPBar;
        }
    }
}