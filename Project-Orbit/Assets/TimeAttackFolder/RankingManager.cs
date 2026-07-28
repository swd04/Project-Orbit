using System.IO;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ランキングデータを管理するクラス
/// </summary>
public class RankingManager : SingletonMonoBehaviour<RankingManager>
{
    [Header("ランキングデータの取得")]
    [SerializeField] private RankingData rankingData = new RankingData();

    [Header("ランキングの最大人数")] // 現状だと10人を設定
    [SerializeField] private int maxRankingCount = 0;

    /// <summary>
    /// セーブする際のパスを取得するプロパティ
    /// </summary>
    private string SavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "ranking.json");
        }
    }

    private void Awake()
    {
        Load();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// プレイヤーを追加する
    /// </summary>
    public void AddPlayer(string name, float clearTime)
    {
        PlayerData player = new PlayerData();

        player.playerNameData = name;
        player.clearTimeData = clearTime;

        rankingData.Players.Add(player);

        // クリアタイムの昇順でソートする
        rankingData.Players.Sort((a, b) => a.clearTimeData.CompareTo(b.clearTimeData));

        // ランキングの最大人数を超えたら削除
        if (rankingData.Players.Count > maxRankingCount)
        {
            rankingData.Players.RemoveRange(maxRankingCount, rankingData.Players.Count - maxRankingCount);
        }

        Save();
    }

    /// <summary>
    /// ランキングを取得する
    /// </summary>
    public List<PlayerData> GetRanking()
    {
        return rankingData.Players;
    }

    /// <summary>
    /// データを保存する
    /// </summary>
    public void Save()
    {
        string json = JsonUtility.ToJson(rankingData, true);
        File.WriteAllText(SavePath, json);
    }

    /// <summary>
    /// データを読み込む
    /// </summary>
    public void Load()
    {
        // ファイルが存在しない場合は新しいランキングデータを作成
        if (!File.Exists(SavePath))
        {
            rankingData = new RankingData();
            return;
        }

        // ファイルが存在する場合は読み込む
        string json = File.ReadAllText(SavePath);

        // JSONをRankingDataに変換
        rankingData = JsonUtility.FromJson<RankingData>(json);

        // 読み込み失敗時
        if (rankingData == null)
        {
            rankingData = new RankingData();
        }

        // Playersがnullの場合
        if (rankingData.Players == null)
        {
            rankingData.Players = new List<PlayerData>();
        }
    }
}