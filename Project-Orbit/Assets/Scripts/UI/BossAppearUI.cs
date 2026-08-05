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

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        CaveatImage.gameObject.SetActive(false);

        bossAppearText.text = "ボス出現！";
    }

    /// <summary>
    /// 
    /// </summary>
    public void Show()
    {
        StartCoroutine(ShowRoutine());
    }

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator ShowRoutine()
    {
        CaveatImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        CaveatImage.gameObject.SetActive(false);
    }
}