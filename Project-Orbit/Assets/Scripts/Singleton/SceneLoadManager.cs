using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移を一元管理するシングルトンクラス
/// </summary>
public class SceneLoadManager : SingletonBehaviour<SceneLoadManager>
{
    [Header("フェード演出のデータ")]
    //フェード演出を管理するデータベース
    [SerializeField] private FadeEffectDatabase fadeEffectDatabase = null;

    //シーンの多重ロード防止フラグ
    private bool isLoading = false;

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
    }

    /// <summary>
    /// シーン読み込み処理
    /// </summary>
    public void LoadScene(SceneType sceneType, FadeType fadeType)
    {
        //すでにロード中の場合は処理しない
        if (isLoading) return;

        //enum名をシーン名として使用
        string sceneName = sceneType.ToString();

        //ビルド設定に存在しないシーンの場合は処理中断
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"{sceneName}はビルド設定で見つかりません");

            return;
        }

        //フェード無し、またはFadeManagerが存在しない場合は即シーン遷移
        if (fadeType == FadeType.None || FadeManager.Instance == null)
        {
            isLoading = true;

            SceneManager.LoadScene(sceneName);

            return;
        }

        //フェード有りの場合はコルーチンで遷移処理を行う
        StartCoroutine(LoadSceneWithFade(sceneName, fadeType));
    }

    /// <summary>
    /// フェードアウト→シーン遷移→フェードインを行う処理
    /// </summary>
    private IEnumerator LoadSceneWithFade(string sceneName, FadeType fadeType)
    {
        //ロード開始
        isLoading = true;

        //フェードデータベースが設定されていない場合は通常遷移
        if (fadeEffectDatabase == null)
        {
            Debug.LogError("FadeEffectDatabase が設定されていません");

            SceneManager.LoadScene(sceneName);

            isLoading = false;

            yield break;
        }

        //使用するフェード演出を取得
        IFadeEffect fadeEffect = fadeEffectDatabase.GetFadeEffect(fadeType);

        //対応するフェード演出が存在しない場合は通常遷移
        if (fadeEffect == null)
        {
            Debug.LogWarning($"FadeType {fadeType} に対応するフェード演出が設定されていません");

            SceneManager.LoadScene(sceneName);

            isLoading = false;

            yield break;
        }

        //フェードアウト
        yield return FadeManager.Instance.FadeOut(fadeEffect);

        //シーン遷移
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        //シーン切り替えを1フレーム待つ
        yield return null;

        //フェードイン
        yield return FadeManager.Instance.FadeIn(fadeEffect);

        //ロード完了
        isLoading = false;
    }
}