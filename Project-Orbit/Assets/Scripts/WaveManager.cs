using System.Collections;
using UnityEngine;

/// <summary>
/// ウェーブ管理クラス
/// </summary>
public class WaveManager : MonoBehaviour
{
    [Header("敵プレハブ")]
    [SerializeField] private GameObject enemyPrefab = null;

    [Header("プレイヤー")]
    [SerializeField] private Transform player = null;

    [Header("敵を出す半径")]
    [SerializeField] private float spawnRadius = 4f;

    [Header("最大ウェーブ数")]
    [SerializeField] private int maxWave = 3;

    [Header("1Waveごとの敵数")]
    [SerializeField] private int enemyCountPerWave = 3;

    [Header("ゲームクリアUI")]
    [SerializeField] private GameObject gameClearUI = null;

    //現在ウェーブ
    private int currentWave = 0;

    //現在生存中の敵数
    private int currentEnemyCount = 0;

    private bool isWaveStarting = false;

    /// <summary>
    /// 開始時
    /// </summary>
    private void Start()
    {
        StartNextWave();
    }

    /// <summary>
    /// 次ウェーブ開始
    /// </summary>
    private void StartNextWave()
    {
        //ウェーブ終了
        if (currentWave >= maxWave)
        {
            Debug.Log("全ウェーブ終了！");

            StartCoroutine(GameClear());

            return;
        }

        currentEnemyCount = 0;

        currentWave++;

        Debug.Log($"Wave {currentWave} 開始");

        //敵生成
        for (int i = 0; i < enemyCountPerWave; i++)
        {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// 敵生成
    /// </summary>
    private void SpawnEnemy()
    {
        //ランダム方向
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;

        //生成位置
        Vector3 spawnPos = player.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

        //敵生成
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        currentEnemyCount++;
    }

    /// <summary>
    /// 敵が倒された時
    /// </summary>
    public void OnEnemyDead()
    {
        //すでに次Wave開始中なら処理しない
        if (isWaveStarting)
        {
            return;
        }

        currentEnemyCount--;

        //全滅したら次のWaveへ
        if (currentEnemyCount <= 0)
        {
            isWaveStarting = true;

            StartCoroutine(StartNextWaveDelay());
        }
    }

    /// <summary>
    /// 次Waveまで少し待つ
    /// </summary>
    private IEnumerator StartNextWaveDelay()
    {
        Debug.Log("Wave Clear!");

        yield return new WaitForSeconds(1f);

        //Wave開始フラグ解除
        isWaveStarting = false;

        StartNextWave();
    }

    /// <summary>
    /// ゲームクリア処理
    /// </summary>
    private IEnumerator GameClear()
    {
        Debug.Log("ゲームクリア!");

        //UI表示
        if (gameClearUI != null)
        {
            gameClearUI.SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        SceneLoadManager.Instance.LoadScene(
            SceneType.GameEndScene,
            FadeType.None);
    }
}