using TMPro;
using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームログ1件分の表示UI
/// </summary>
public class GameLogItem : MonoBehaviour
{
    [Header("メッセージテキスト")]
    [SerializeField] private TMP_Text messageText = null;

    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroup = null;

    //RectTransform
    private RectTransform rectTransform;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //RectTransform取得
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// ログ初期化処理
    /// </summary>
    public void Initialize(string message)
    {
        //メッセージ設定
        messageText.text = message;

        //表示アニメーション開始
        StartCoroutine(PlayAnimation());
    }

    /// <summary>
    /// フェードインアニメーション
    /// </summary>
    private IEnumerator PlayAnimation()
    {
        float duration = 0.25f;
        float timer = 0f;

        //透明状態から開始
        canvasGroup.alpha = 0f;

        //徐々に透明度を上げる
        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            canvasGroup.alpha = t;

            yield return null;
        }

        //完全表示
        canvasGroup.alpha = 1f;
    }

    /// <summary>
    /// フェードアウトアニメーション
    /// </summary>
    public IEnumerator FadeOut(float duration)
    {
        float timer = 0f;

        //開始スケール
        Vector3 startScale = Vector3.one;

        //終了スケール
        Vector3 endScale = Vector3.one * 0.9f;

        //徐々に透明化しながら縮小
        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            canvasGroup.alpha = 1f - t;

            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        //完全非表示
        canvasGroup.alpha = 0f;

        //最終スケール設定
        transform.localScale = endScale;
    }
}