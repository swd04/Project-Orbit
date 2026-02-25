using UnityEngine;

public class CoreEffectScript : MonoBehaviour
{
    [Header("パーティクルの取得")]
    [SerializeField] private ParticleSystem particle = null;

    [Header("現在のパーティクル再生時間")]
    [SerializeField] private float currentParticlePlayTime = 0.0f;

    [Header("パーティクル間隔")]
    [SerializeField] private float duration = 0.0f;

    [Header("逆再生する判定")]
    [SerializeField] private bool isPlayingReverse = false;


    void Start()
    {
        // パーティクルの間隔を変数に入れる
        duration = particle.main.duration;

        // 最後の状態を作っておく
        particle.Simulate(duration, true, true);
        particle.Pause();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartReverse();
        }

        if (isPlayingReverse)
        {
            ParticleTime();
        }


    }


    private void StartReverse()
    {
        currentParticlePlayTime = duration;
        isPlayingReverse = true;
    }

    /// <summary>
    /// パーティクル逆再生の処理
    /// </summary>
    private void ParticleTime()
    {
        // パーティクルが再生中の時に処理が開始される
        if (currentParticlePlayTime > 0.0f)
        {
            // パーティクルの再生時間を巻き戻しにする
            currentParticlePlayTime -= Time.deltaTime;

            // それを再計算させる
            particle.Simulate(currentParticlePlayTime, true, true);
        }
        else
        {
            isPlayingReverse = false;
        }
    }
}
