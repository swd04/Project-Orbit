using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BGMとSEの音量調整をするクラス
/// </summary>
public class AudioSettingUI : MonoBehaviour
{
    [Header("BGM音量Slider")]
    [SerializeField] private Slider bgmVolumeSlider = null;

    [Header("BGM音量表示Text")]
    [SerializeField] private TMP_Text bgmVolumeText = null;

    [Header("SE音量Slider")]
    [SerializeField] private Slider seVolumeSlider = null;

    [Header("SE音量表示Text")]
    [SerializeField] private TMP_Text seVolumeText = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //BGMの音量変更イベントを登録
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);

        //SEの音量変更イベントを登録
        seVolumeSlider.onValueChanged.AddListener(SetSEVolume);
    }

    /// <summary>
    /// BGMの音量を変更する処理
    /// </summary>
    private void SetBGMVolume(float volume)
    {
        //BGMの音量を変更
        BGMManager.Instance.SetVolume(volume);

        //BGMの音量表示を更新
        UpdateBGMVolumeText(volume);
    }

    /// <summary>
    /// SEの音量を変更する処理
    /// </summary>
    private void SetSEVolume(float volume)
    {
        //SEの音量を変更
        SEManager.Instance.SetVolume(volume);

        //SEの音量表示を更新
        UpdateSEVolumeText(volume);
    }

    /// <summary>
    /// BGMの音量表示を更新する処理
    /// </summary>
    private void UpdateBGMVolumeText(float volume)
    {
        //音量を0～100の数値に変換して表示
        bgmVolumeText.text = Mathf.RoundToInt(volume * 100.0f) + "%";
    }

    /// <summary>
    /// SEの音量表示を更新する処理
    /// </summary>
    private void UpdateSEVolumeText(float volume)
    {
        //音量を0～100の数値に変換して表示
        seVolumeText.text = Mathf.RoundToInt(volume * 100.0f) + "%";
    }

    /// <summary>
    /// 設定画面を表示したときの処理
    /// </summary>
    private void OnEnable()
    {
        //現在のBGM音量をSliderに反映
        bgmVolumeSlider.SetValueWithoutNotify(BGMManager.Instance.GetVolume());

        //現在のSE音量をSliderに反映
        seVolumeSlider.SetValueWithoutNotify(SEManager.Instance.GetVolume());

        //現在のBGM音量をTextに反映
        UpdateBGMVolumeText(BGMManager.Instance.GetVolume());

        //現在のSE音量をTextに反映
        UpdateSEVolumeText(SEManager.Instance.GetVolume());
    }

    /// <summary>
    /// 破棄時の後処理
    /// </summary>
    private void OnDestroy()
    {
        //BGMの音量変更イベントを解除
        if (bgmVolumeSlider != null)
        {
            bgmVolumeSlider.onValueChanged.RemoveListener(SetBGMVolume);
        }

        //SEの音量変更イベントを解除
        if (seVolumeSlider != null)
        {
            seVolumeSlider.onValueChanged.RemoveListener(SetSEVolume);
        }
    }
}