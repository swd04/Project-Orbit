using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ダメージを受けた際のフラッシュ演出クラス
/// </summary>
public class DamageFlashUI : MonoBehaviour
{
    [Header("フラッシュ用画像")]
    //画面全体を覆うImage
    [SerializeField] private Image overlayImage;

    [Header("フラッシュ時の最大アルファ値")]
    [SerializeField] private float flashAlpha = 0.5f;

    [Header("フェードアウトにかかる時間")]
    [SerializeField] private float fadeDuration = 0.2f;

    //実行中のフラッシュコルーチン管理用
    Coroutine flashCoroutine;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //赤色・アルファ0にする
        if (overlayImage != null)
        {
            overlayImage.color = new Color(1f, 0f, 0f, 0f);
        }
    }

    /// <summary>
    /// ダメージフラッシュを再生する処理
    /// </summary>
    public void PlayDamageFlash()
    {
        if (overlayImage == null) return;

        //既に再生中なら止める
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        //フラッシュ演出開始
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    /// <summary>
    /// フラッシュ演出処理
    /// </summary>
    private IEnumerator FlashRoutine()
    {
        //フラッシュ開始時の色
        overlayImage.color = new Color(1f, 0f, 0f, flashAlpha);

        float t = 0f;

        //フェードアウト処理
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(flashAlpha, 0f, t / fadeDuration);
            SetAlpha(alpha);

            yield return null;
        }

        //完全に透明に戻す
        SetAlpha(0f);

        //コルーチン終了
        flashCoroutine = null;
    }

    /// <summary>
    /// オーバーレイ画像のアルファ値のみを変更する処理
    /// </summary>
    private void SetAlpha(float alpha)
    {
        Color c = overlayImage.color;
        c.a = alpha;
        overlayImage.color = c;
    }
}