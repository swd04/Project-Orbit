using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ゲーム内メッセージ表示UI
/// </summary>
public class GameLogUI : MonoBehaviour
{
    public static GameLogUI Instance { get; private set; }

    [Header("ログ親")]
    [SerializeField] private Transform contentParent = null;

    [Header("ログプレハブ")]
    [SerializeField] private GameLogItem logPrefab = null;

    [Header("最大ログ数")]
    [SerializeField] private int maxLogCount = 10;

    [Header("表示時間")]
    [SerializeField] private float displayTime = 1f;

    //生成中のログ一覧
    private readonly List<GameLogItem> logs = new();

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    /// <summary>
    /// ログ追加
    /// </summary>
    public void AddLog(string message)
    {
        //ログ生成
        GameLogItem log = Instantiate(logPrefab, contentParent);

        //メッセージ設定
        log.Initialize(message);

        //ログ一覧に追加
        logs.Add(log);

        //最大ログ数を超えた場合は最も古いログを削除
        if (logs.Count > maxLogCount)
        {
            GameLogItem oldestLog = logs[0];

            logs.RemoveAt(0);

            Destroy(oldestLog.gameObject);
        }

        //一定時間後に削除
        StartCoroutine(RemoveLogRoutine(log));
    }

    /// <summary>
    /// 一定時間後にログ削除
    /// </summary>
    private IEnumerator RemoveLogRoutine(GameLogItem log)
    {
        //表示時間待機
        yield return new WaitForSeconds(displayTime);

        //ログが存在する場合のみ削除
        if (log != null)
        {
            //フェードアウト
            yield return StartCoroutine(log.FadeOut(0.3f));

            //ログ一覧から削除
            logs.Remove(log);

            //ログオブジェクト削除
            Destroy(log.gameObject);
        }
    }
}