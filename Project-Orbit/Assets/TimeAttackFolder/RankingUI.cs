using UnityEngine;
using TMPro;

public class RankingUI : MonoBehaviour
{
    [Header("今回のプレイヤー名")]
    [SerializeField] private TMP_Text resultNameText = null;

    [Header("今回のデータ")]
    [SerializeField] private UserDataHolder userDataHolder = null;

    [Header("ランキングの表示")]
    [SerializeField] private TMP_Text[] nameAndClearTimeTexts = null;

    private void Start()
    {
        userDataHolder = FindAnyObjectByType<UserDataHolder>();
        // 今回の結果を表示
        resultNameText.text = "プレイヤー名 : " + userDataHolder.userName + "クリアタイム : " + userDataHolder.clearTime.ToString("F2") + "秒";

        // ランキングを表示
        RankingDisplay();
    }

    /// <summary>
    /// ランキング表示
    /// </summary>
    public void RankingDisplay()
    {
        var ranking = RankingManager.Instance.GetRanking();

        for (int i = 0; i < nameAndClearTimeTexts.Length; i++)
        {
            if (i < ranking.Count)
            {
                nameAndClearTimeTexts[i].text = (i + 1) + "位 : " + ranking[i].playerNameData + " : " + ranking[i].clearTimeData.ToString("F2") + "秒";
            }
            else
            {
                nameAndClearTimeTexts[i].text = (i + 1) + "位 : No Data";
            }
        }
    }
}