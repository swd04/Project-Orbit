using System.Collections.Generic;
using UnityEngine;

public class CoreCollection : MonoBehaviour
{
    [Header("プレイヤーステータス")]
    [SerializeField] private PlayerStatus playerStatus = null;

    [Header("コアの総数")]
    [SerializeField] public int coreCount = 0;

    [Header("プレイヤーの武器モード")]
    [SerializeField] private PlayerChoseAttackMode playerChoseAttackMode = null;

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
        playerStatus.AddSoulCore(soulCore);

        //回収したコアを消す
        other.gameObject.SetActive(false);

        //ログ表示
        GameLogUI.Instance.AddLog($"{soulCore.actionType}を取得");

        Debug.Log($"{soulCore.actionType}を取得" +
            $"現在所持数:{coreDictionary[soulCore.actionType]}");

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
}