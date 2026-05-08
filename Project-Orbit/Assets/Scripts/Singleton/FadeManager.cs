using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// フェードイン・フェードアウトを管理する司令塔クラス
/// 実際のフェード演出はIFadeEffectに委譲する
/// </summary>
public class FadeManager : SingletonBehaviour<FadeManager>
{
    [Header("フェード演出に使用するUI")]
    //フェード演出に使用するUI
    //画面全体を覆うImageを想定
    [SerializeField] private Image fadeImage = null;

    [Header("UI操作をロックしたいCanvas")]
    //フェード中にUI操作をロックしたい場合に使用するCanvasGroup
    //interactableとblocksRaycastsを切り替えてUI操作を無効化
    [SerializeField] private CanvasGroup uiCanvasGroup;

    //フェード演出が再生中かどうかを管理するフラグ
    //多重再生防止用
    private bool isPlaying = false;

    /// <summary>
    /// フェード中に入力をロックしているかを外部から確認可能にする
    /// </summary>
    public bool IsInputLocked { get; private set; }

    //シーン遷移後もこのオブジェクトを保持するかどうか
    protected override bool UseDontDestroy => true;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //シングルトンの初期化処理
        base.Awake();

        //重複インスタンスの場合は初期化しない
        if (!IsValidInstance) return;

        //fadeImageが設定されてない時
        if (fadeImage == null)
        {
            Debug.LogError($"FadeManager: fadeImageが設定されていません（Scene: {gameObject.scene.name}）");
        }
    }

    /// <summary>
    /// オブジェクト破棄時の後片付け処理
    /// フェード中フラグやUIロックを解除
    /// </summary>
    private void OnDestroy()
    {
        //自分自身がシングルトンだった場合
        if (Instance == this)
        {
            //入力ロック解除
            IsInputLocked = false;

            //UI操作可能に戻す
            if (uiCanvasGroup != null)
            {
                uiCanvasGroup.interactable = true;
                uiCanvasGroup.blocksRaycasts = true;
            }
        }
    }

    /// <summary>
    /// フェード状態の管理
    /// </summary>
    private void SetFadeState(bool playing)
    {
        //フェード再生中フラグを更新
        isPlaying = playing;

        //入力ロック状態も更新
        IsInputLocked = playing;

        //CanvasGroupが設定されていればUI操作もロック
        if (uiCanvasGroup != null)
        {
            uiCanvasGroup.interactable = !playing;
            uiCanvasGroup.blocksRaycasts = !playing;
        }
    }

    /// <summary>
    /// フェードアウト処理
    /// 指定されたIFadeEffectに処理を委譲する
    /// フェード状態は解除しない
    /// 必ずFadeInとセットで使用すること
    /// </summary>
    public IEnumerator FadeOut(IFadeEffect fadeEffect)
    {
        //再生中、または不正な状態の場合は何もしない
        if (isPlaying || fadeEffect == null || fadeImage == null)
        {
            yield break;
        }

        //フェード開始状態に設定
        SetFadeState(true);

        //フェード用Imageを有効化
        fadeImage.gameObject.SetActive(true);

        //フェードアウト演出を実行
        yield return fadeEffect.FadeOut(fadeImage);
    }

    /// <summary>
    /// フェードイン処理
    /// フェード完了後にUIを非表示・入力解除する
    /// </summary>
    public IEnumerator FadeIn(IFadeEffect fadeEffect)
    {
        //再生中、または不正な状態の場合は何もしない
        if (fadeEffect == null || fadeImage == null)
        {
            yield break;
        }

        //フェードイン演出を実行
        yield return fadeEffect.FadeIn(fadeImage);

        //フェードイン終了後、Imageを非表示にする
        fadeImage.gameObject.SetActive(false);

        //フェード状態を解除
        SetFadeState(false);
    }
}