using UnityEngine;

/// <summary>
/// BGMを管理するシングルトンクラス
/// </summary>
public class BGMManager : SingletonBehaviour<BGMManager>
{
    [Header("BGM再生用AudioSource")]
    [SerializeField] private AudioSource audioSource = null;

    [Header("BGM音量")]
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
    /// BGMを再生する処理
    /// </summary>
    public void PlayBGM(AudioClip clip)
    {
        //BGMが設定されていなければ処理を終了
        if (clip == null)
        {
            return;
        }

        //同じBGMが既に再生されている場合は処理を終了
        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            return;
        }

        //BGMを設定
        audioSource.clip = clip;

        //BGMを再生
        audioSource.Play();
    }

    /// <summary>
    /// BGMを停止する処理
    /// </summary>
    public void StopBGM()
    {
        //BGMを停止
        audioSource.Stop();
    }

    /// <summary>
    /// BGMの音量を設定する処理
    /// </summary>
    public void SetVolume(float volume)
    {
        //音量を0～1の範囲に制限
        this.volume = Mathf.Clamp01(volume);

        //AudioSourceの音量を変更
        audioSource.volume = this.volume;
    }

    /// <summary>
    /// BGMの音量を取得する処理
    /// </summary>
    public float GetVolume()
    {
        return volume;
    }
}