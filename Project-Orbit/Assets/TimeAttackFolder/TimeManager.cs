using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void Start()
    {
        countdownDisplay.SetActive(true);
    }

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

            if (timer < startTextDisplayTime)
            {
                startText.gameObject.SetActive(true);
            }
            else
            {
                startText.gameObject.SetActive(false);
            }
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
}
