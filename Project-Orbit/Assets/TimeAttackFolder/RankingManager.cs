using NUnit.Framework;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class RankingManager : SingletonMonoBehaviour<RankingManager>
{
    [Header("ランキングデータの取得")]
    [SerializeField] private RankingData rankingData = new RankingData();

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
    }

    /// <summary>
    /// プレイヤーを追加する
    /// </summary>
    public void AddPlayer(string name, float clearTime)
    {
        PlayerData player = new PlayerData();

        player.playerNameData = name;
        player.clearTimeData = clearTime;

        GameClear.Instance.GetUserData(name, clearTime);

        rankingData.Players.Add(player);

        // クリアタイムの昇順でソートする
        rankingData.Players.Sort((a, b) => a.clearTimeData.CompareTo(b.clearTimeData));

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
        if (!File.Exists(SavePath))
        {
            rankingData = new RankingData();
            return;
        }

        string json = File.ReadAllText(SavePath);
        rankingData = JsonUtility.FromJson<RankingData>(json);
    }
}
