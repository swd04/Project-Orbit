using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通常のフェード演出クラス
/// </summary>
[CreateAssetMenu(
    fileName = "AlphaFadeEffect",
    menuName = "AlphaFades/AlphaFadeEffect")]
public class AlphaFadeEffect : ScriptableObject, IFadeEffect
{
    [Header("フェードアウト時間")]
    [SerializeField] private float fadeOutTime = 0.0f;

    [Header("フェードイン時間")]
    [SerializeField] private float fadeInTime = 0.0f;

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    public IEnumerator FadeOut(Image image)
    {
        //Imageが設定されていなければ処理を終了
        if (image == null)
        {
            yield break;
        }

        //現在のImageの色を取得
        Color color = image.color;

        //フェード開始時のアルファ値を取得
        float startAlpha = color.a;

        //フェード開始からの経過時間
        float elapsedTime = 0.0f;

        //指定したフェード時間が経過するまで繰り返す
        while (elapsedTime < fadeOutTime)
        {
            //経過時間を加算
            elapsedTime += Time.deltaTime;

            //フェードの進行割合を計算
            float rate = elapsedTime / fadeOutTime;

            //アルファ値を現在の値から1.0まで徐々に変更
            color.a = Mathf.Lerp(startAlpha, 1.0f, rate);

            //変更した色をImageへ反映
            image.color = color;

            //次のフレームまで待機
            yield return null;
        }

        //フェード完了後、アルファ値を完全に不透明にする
        color.a = 1.0f;

        //最終的な色をImageへ反映
        image.color = color;
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    public IEnumerator FadeIn(Image image)
    {
        //Imageが設定されていなければ処理を終了
        if (image == null)
        {
            yield break;
        }

        //現在のImageの色を取得
        Color color = image.color;

        //フェード開始時のアルファ値を取得
        float startAlpha = color.a;

        //フェード開始からの経過時間
        float elapsedTime = 0.0f;

        //指定したフェード時間が経過するまで繰り返す
        while (elapsedTime < fadeInTime)
        {
            //経過時間を加算
            elapsedTime += Time.deltaTime;

            //フェードの進行割合を計算
            //0.0 = 開始時、1.0 = 完了
            float rate = elapsedTime / fadeInTime;

            //アルファ値を現在の値から0.0まで徐々に変更
            color.a = Mathf.Lerp(startAlpha, 0.0f, rate);

            //変更した色をImageへ反映
            image.color = color;


            //次のフレームまで待機
            yield return null;
        }

        //フェード完了後、アルファ値を完全に透明にする
        color.a = 0.0f;

        //最終的な色をImageへ反映
        image.color = color;
    }
}