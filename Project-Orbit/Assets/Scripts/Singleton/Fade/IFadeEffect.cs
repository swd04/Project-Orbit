using System.Collections;
using UnityEngine.UI;

/// <summary>
/// フェード演出の共通インターフェース
/// FadeManagerから呼び出される
/// </summary>
public interface IFadeEffect
{
    /// <summary>
    /// フェードアウト処理
    /// </summary>
    IEnumerator FadeOut(Image image);

    /// <summary>
    /// フェードイン処理
    /// </summary>
    IEnumerator FadeIn(Image image);
}