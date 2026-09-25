using UnityEngine;

/// <summary>
/// SEを管理するシングルトンクラス
/// </summary>
public class SEManager : SingletonBehaviour<SEManager>
{
    [Header("SE再生用AudioSource")]
    [SerializeField] private AudioSource audioSource = null;

    [Header("SE音量")]
    [SerializeField, Range(0.0f, 1.0f)]
    private float volume = 1.0f;

    /// <summary>
    /// シーンを跨いで保持するかどうか
    /// </summary>
    protected override bool UseDontDestroy => true;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //シングルトンとして初期化
        base.Awake();

        //有効なインスタンスでなければ処理を終了
        if (!IsValidInstance)
        {
            return;
        }

        //初期音量を設定
        audioSource.volume = volume;
    }

    /// <summary>
    /// SEを再生する処理
    /// </summary>
    public void PlaySE(AudioClip clip)
    {
        //SEが設定されていなければ処理を終了
        if (clip == null)
        {
            return;
        }

        //SEを再生
        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// SEの音量を設定する処理
    /// </summary>
    public void SetVolume(float volume)
    {
        //音量を0～1の範囲に制限
        this.volume = Mathf.Clamp01(volume);

        //AudioSourceの音量を変更
        audioSource.volume = this.volume;
    }

    /// <summary>
    /// SEの音量を取得する処理
    /// </summary>
    public float GetVolume()
    {
        return volume;
    }
}