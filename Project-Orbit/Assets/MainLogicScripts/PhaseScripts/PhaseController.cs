using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// フェーズ管理クラス
/// </summary>
public class PhaseController : MonoBehaviour
{
    [Header("フェーズの設定時間")]
    [SerializeField] private float phaseTime = 0.0f;

    [Header("プレイヤーオブジェクトの取得")]
    [SerializeField] private GameObject player = null;

    [Header("タイマー")]
    [SerializeField] private float timer = 0.0f;

    [Header("現在のフェーズ番号")]
    [SerializeField] private int phaseCount = 0;

    [Header("最大のフェーズ数")]
    [SerializeField] private int maxPhaseCount = 0;

    [Header("フェーズごとに出現させたい敵を設定(スクリプタブルオブジェクトをアタッチすること)")]
    [SerializeField] private List<PhaseSpwanEnemyDate> phaseSpwanEnemys = new List<PhaseSpwanEnemyDate>();

    [Header("敵を生成させるファクトリー")]
    [SerializeField] private PhaseEnemyFactory factory = null;

    [Header("現在進行のフェーズに出現している敵のリスト")]
    [SerializeField] private List<EnemyStatus> nowPhaseEnemyObjects = new List<EnemyStatus>();

    [Header("フェーズが進行しているかのトリガー")]
    [SerializeField] private bool isPhaseStartFlag = false;

    [Header("フェーズ切り替え中のトリガー")]
    [SerializeField] private bool isPhaseChanging = false;

    [Header("フェーズ変更までにかかる時間")]
    [SerializeField] private float phaseChangeTime = 0.0f;

    [Header("フェーズ進行中プレイヤーが外に出ないための障壁オブジェクトリスト")]
    [SerializeField] private List<GameObject> phaseBoderObject = new List<GameObject>();

    [Header("EnemyAIControllerの取得")]
    [SerializeField] private EnemyAIController enemyAIController = null;

    [Header("出現している敵の最大数")]
    [SerializeField] private int phaseEnemyMaxCount = 0;

    [Header("勢力ゲージの値")]
    [SerializeField] private float gaugeCount = 0.0f;

    [Header("勢力ゲージのImage")]
    [SerializeField] private Image gauge = null;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        PhaseEnemySpwan();
        isPhaseStartFlag = true;
    }
    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        gaugeCount = (float)nowPhaseEnemyObjects.Count / (float)phaseEnemyMaxCount;
        gauge.DOFillAmount(gaugeCount, 1.0f);
        if (isPhaseStartFlag)
        {
           
            if (nowPhaseEnemyObjects.Count <= 0 && !isPhaseChanging && phaseCount <= maxPhaseCount)
            {
                StartCoroutine(PhaseChange());
            }
        }
    }

    /// <summary>
    /// フェーズ進行時に敵を生成する処理
    /// </summary>
    public void PhaseEnemySpwan()
    {
        for (int i = 0; i < phaseSpwanEnemys[phaseCount].spwanEnemyTypes.Count; i++)
        {
            for(int j = 0; j < phaseSpwanEnemys[phaseCount].spwanEnemyCount[i];j++)
            {

                var enemy = factory.CreateEnemyObject(phaseSpwanEnemys[phaseCount].spwanEnemyTypes[i]).GetComponent<EnemyStatus>();

                
                enemyAIController.InitializeTarget(player.transform);

                enemy.PhaseControllerSet(this);
                enemy.transform.parent = transform;
                if (phaseSpwanEnemys[phaseCount].spwanEnemyStatusBounus[i])
                {
                    enemy.EnemyPhaseStatusBounus();
                }
                nowPhaseEnemyObjects.Add(enemy);
            }
        }
        phaseEnemyMaxCount = nowPhaseEnemyObjects.Count;
        for(int i = 0; i < nowPhaseEnemyObjects.Count;i++)
        {
            nowPhaseEnemyObjects[i].transform.position = transform.position + new Vector3(Random.Range(-10,10),0, Random.Range(-10, 10));
        }    
    }

    /// <summary>
    /// 倒された敵が自身をリストから削除するメソッド
    /// </summary>
    /// <param name="enemy"></param>
    public void PhaseEnemyRemove(EnemyStatus enemy)
    {
        nowPhaseEnemyObjects.Remove(enemy);
    }

    /// <summary>
    /// フェーズ開始メソッド
    /// </summary>
    public void PhaseStart()
    {
        isPhaseChanging = true;
        for(int i = 0;i < phaseBoderObject.Count; i++)
        {
            phaseBoderObject[i].SetActive(true);
        }
    }

    /// <summary>
    /// フェーズ変更コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator PhaseChange()
    {
        isPhaseChanging = true;

        phaseCount++;

        yield return new WaitForSeconds(phaseChangeTime);

        PhaseEnemySpwan();

        yield return new WaitForSeconds(3.0f);

        isPhaseChanging = false;
    }
}
