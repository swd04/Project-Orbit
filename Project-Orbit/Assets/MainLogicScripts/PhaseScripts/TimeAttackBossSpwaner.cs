using TMPro;
using UnityEngine;

public class TimeAttackBossSpwaner : MonoBehaviour
{
    [Header("ボスが出現するまでの敵死亡数")]
    [SerializeField] private int bossSpwanCount = 0;

    [Header("敵の合計死亡数")]
    [SerializeField] public int deathEnemyCount = 0;

    [Header("ボスが出現したかのフラグ")]
    [SerializeField] public bool isBossSpwan = false;

    [Header("ボスのオブジェクト")]
    [SerializeField] private GameObject bossObject = null;

    [SerializeField] private TextMeshProUGUI countText = null;
    private void Update()
    {
        BossSpwan();


        countText.text = $"{bossSpwanCount - deathEnemyCount}体";
    }

    public void BossSpwan()
    {
        if (!isBossSpwan)
        {
            if(bossSpwanCount <= deathEnemyCount)
            {
                bossObject.SetActive(true);
            }
        }
    }
}
