using System.Collections.Generic;
using UnityEngine;

public class CoreCollection : MonoBehaviour
{
    //[Header("プレイヤーステータス")]
    //[SerializeField] private PlayerStatus playerStatus = null;

    [Header("コアの総数")]
    [SerializeField] public int coreCount = 0;

    [Header("プレイヤーの武器モード")]
    [SerializeField] private PlayerChoseAttackMode playerChoseAttackMode = null;

    [SerializeField] public List<SoulCore> soulCoresList = new List<SoulCore>();

    //種類別所持数
    private Dictionary<SoulCore.ActionType, int> coreDictionary = new();

    /// <summary>
    /// コア総数
    /// </summary>
    public int CoreCount => coreCount;

    //private void Start()
    //{
    //    // コア取得数の初期化
    //    coreCount = 0;
    //}

    /// <summary>
    /// 指定種類の所持数取得処理
    /// </summary>
    public int GetCoreCount(SoulCore.ActionType type)
    {
        //指定した種類のコアが登録されている場合
        if (coreDictionary.TryGetValue(type, out int count))
        {
            return count;
        }

        return 0;
    }

    /// <summary>
    /// コア取得と取得した際にコア取得数を増やす処理
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        //ソウル以外なら処理しない
        if (!other.CompareTag("Soul"))
        {
            return;
        }

        //SoulCore取得
        SoulCore soulCore = other.GetComponent<SoulCore>();

        //SoulCoreが付いていない場合は終了
        if (soulCore == null)
        {
            return;
        }

        //総取得数を増加
        coreCount++;

        //初めて取得した種類なら登録
        if (!coreDictionary.ContainsKey(soulCore.actionType))
        {
            coreDictionary.Add(soulCore.actionType, 0);
        }

        //種類別取得数を増加
        coreDictionary[soulCore.actionType]++;

        //PlayerStatusへ登録
        AddSoulCore(soulCore);

        //回収したコアを消す
        other.gameObject.SetActive(false);

        //
        string coreName = soulCore.gameObject.name.Replace("(Clone)", "");

        //ログ表示
        GameLogUI.Instance.AddLog($"{soulCore.gameObject.name}を取得");

        Debug.Log($"{soulCore.gameObject.name}を取得" + $"現在所持数:{coreDictionary[soulCore.actionType]}");

        //if (playerChoseAttackMode.currentAttackMode == AttackMode.SOULREINFORCE)
        //{
        //    // コアに触れたとき、コア取得数を増やす
        //    if (other.gameObject.CompareTag("Core"))
        //    {
        //        coreCount++;
        //        Debug.Log("コアを取得しました 現在のコア数: " + coreCount);
        //    }
        //}
    }

    /// <summary>
    /// ソウルコアを登録する処理
    /// </summary>
    public void AddSoulCore(SoulCore soulCore)
    {
        //同じ種類のコアを所持しているか確認
        foreach (SoulCore core in soulCoresList)
        {
            //同じ種類ならレベルアップして回収
            if (core.actionType == soulCore.actionType)
            {
                core.SoulLevelUp();

                //回収したコアを非表示
                soulCore.gameObject.SetActive(false);

                return;
            }
        }

        //初めて取得した種類なので登録
        soulCoresList.Add(soulCore);

        //回収したコアを非表示
        soulCore.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public SoulCore GetSoulCore(SoulCore.ActionType type)
    {
        //
        foreach (SoulCore core in soulCoresList)
        {
            //
            if (core.actionType == type)
            {
                return core;
            }
        }

        return null;
    }
}