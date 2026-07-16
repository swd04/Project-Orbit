using NUnit.Framework;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour
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

    public void AddPlayer(string name, float clearTime)
    {
        PlayerData player = new PlayerData();

        player.playerNameData = name;
        player.clearTimeData = clearTime;

        rankingData.Players.Add(player);

        rankingData.Players.Sort((a, b) => a.clearTimeData.CompareTo(b.clearTimeData));

        Save();
    }

    public List<PlayerData> GetRanking()
    {
        return rankingData.Players;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(rankingData, true);
        File.WriteAllText(SavePath, json);
    }

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
