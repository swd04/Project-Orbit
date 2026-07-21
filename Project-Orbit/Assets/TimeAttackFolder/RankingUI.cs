using UnityEngine;
using TMPro;

public class RankingUI : MonoBehaviour
{
    [Header("名前の表示")]
    [SerializeField] private TMP_Text[] nameAndClearTimeTexts = null;

    private void Start()
    {
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
                nameAndClearTimeTexts[i].text = (i + 1) + "位 : " + "NoData" + "NoData";
            }
        }

    }
}
