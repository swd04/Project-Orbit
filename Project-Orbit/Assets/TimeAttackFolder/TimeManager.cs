using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// タイムアタック内での時間を管理するクラス
/// </summary>
public class TimeManager : SingletonBehaviour<TimeManager>
{
    [Header("タイマー")]
    [SerializeField] private float timer = 0f;

    [Header("何秒からカウントダウンするかを設定")]
    [SerializeField] private float countdownTime = 0;

    [Header("カウントダウンのBG")]
    [SerializeField] private GameObject countdownDisplay = null;

    [Header("タイマーBG")]
    [SerializeField] private GameObject timerDisplay = null;

    [Header("タイマーテキスト")]
    [SerializeField] private TextMeshProUGUI timerText = null;

    [Header("カウントダウンテキスト")]
    [SerializeField] private TextMeshProUGUI countDownText = null;

    [Header("開始を知らせる表示")]
    [SerializeField] private TextMeshProUGUI startText = null;

    [Header("開始テキストを何秒間表示させるか")]
    [SerializeField] private float startTextDisplayTime = 0f;

    [Header("UserDataHolder")]
    [SerializeField] private UserDataHolder userDataHolder = null;

    [SerializeField] private GameObject gameMainObject = null;

    [SerializeField] private GameObject countDownCamera = null;

    /// <summary>
    /// カウントダウンを最初に表する
    /// </summary>
    private void Start()
    {
        countdownDisplay.SetActive(true);

        userDataHolder = FindAnyObjectByType<UserDataHolder>();
    }

    /// <summary>
    /// 表示の切り替えを毎フレーム更新
    /// </summary>
    private void Update()
    {
       


        TimerSceneDisplay();

        countdownTime -= Time.deltaTime;

        if (countdownTime <= 0)
        {
            timer += Time.deltaTime;
        }

        if (countdownTime < 0)
        {
            countdownDisplay.SetActive(false);
            timerDisplay.SetActive(true);
            countDownCamera.SetActive(false);
            gameMainObject.SetActive(true);

            if (timer < startTextDisplayTime)
            {
                startText.gameObject.SetActive(true);
            }
            else
            {
                startText.gameObject.SetActive(false);
            }
        }

        // 仮のゲームクリア処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameClear();
        }
    }

    public void TimerSceneDisplay()
    {
        countDownText.text = countdownTime.ToString("0");
        timerText.text = timer.ToString("0.00");
    }

    public float GetClearTime()
    {
        return timer;
    }
    /// <summary>
    /// ゲームクリア時の処理
    /// </summary>
    public void GameClear()
    {
        // 今回のクリアタイムを保存
        userDataHolder.clearTime = timer;

        // ランキングへ登録
        RankingManager.Instance.AddPlayer(userDataHolder. userName, userDataHolder.clearTime);

        // ランキング画面へ遷移
        SceneManager.LoadScene("RankingScene");
    }
}