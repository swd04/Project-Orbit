using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// ボス出現演出用UI
/// </summary>
public class BossAppearUI : MonoBehaviour
{
    [Header("ボス出現演出用UI")]
    [SerializeField] private GameObject CaveatImage = null;

    [Header("ボス出現テキスト")]
    [SerializeField] private TMP_Text bossAppearText = null;

    [Header("表示時間")]
    [SerializeField] private float displayTime = 1.0f;

    [Header("BGMデータ")]
    [SerializeField] private BGMData bgmData = null;

    private bool isBGMPlayed = false;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        CaveatImage.gameObject.SetActive(false);

        bossAppearText.text = "ボス出現！";
    }

    /// <summary>
    /// ボス出現演出を表示する処理
    /// </summary>
    public void Show()
    {
        StartCoroutine(ShowRoutine());
    }

    /// <summary>
    /// ボス出現演出を表示するコルーチン
    /// </summary>
    private IEnumerator ShowRoutine()
    {
        //ボス出現演出用UIを表示
        CaveatImage.gameObject.SetActive(true);

        //BGMを一度だけ再生
        if (!isBGMPlayed)
        {
            BGMManager.Instance.PlayBGM(bgmData.GetBGM(BGMType.Boss));
            isBGMPlayed = true;
        }

        //指定した時間待機
        yield return new WaitForSeconds(displayTime);

        //ボス出現演出用UIを非表示
        CaveatImage.gameObject.SetActive(false);
    }
}