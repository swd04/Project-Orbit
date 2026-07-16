using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonBehaviour<TimeManager>
{
    [Header("タイマー")]
    [SerializeField] private float timer = 0f;

    [Header("何秒からカウントダウンするかを設定")]
    [SerializeField] private int countdownTime = 0;

    private void Update()
    {
        countdownTime--;

        if (countdownTime <= 0)
        {
            timer += Time.deltaTime;
        }
    }

    public float GetClearTime()
    {
        return timer;
    }
}
