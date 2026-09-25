using UnityEngine;
using System;

/// <summary>
/// ゲーム内で使用するBGMを管理するクラス
/// </summary>
[CreateAssetMenu(fileName = "BGMData", menuName = "Audio/BGMData")]
public class BGMData : ScriptableObject
{
    [Header("BGM一覧")]
    [SerializeField] private BGMEntry[] bgmEntries = null;

    /// <summary>
    /// 指定した種類のBGMを取得する処理
    /// </summary>
    public AudioClip GetBGM(BGMType type)
    {
        //BGM一覧から指定した種類のBGMを検索
        foreach (BGMEntry entry in bgmEntries)
        {
            //種類が一致した場合
            if (entry.type == type)
            {
                //BGMを返す
                return entry.clip;
            }
        }

        //BGMが見つからなかった場合
        return null;
    }

    /// <summary>
    /// BGMデータ
    /// </summary>
    [Serializable]
    private class BGMEntry
    {
        [Header("BGM種類")]
        public BGMType type = BGMType.Title;

        [Header("BGM")]
        public AudioClip clip = null;
    }
}