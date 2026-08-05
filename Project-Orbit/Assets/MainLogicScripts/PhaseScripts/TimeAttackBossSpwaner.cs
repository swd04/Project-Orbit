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

    [Header("目標UI")]
    [SerializeField] private ObjectiveUI objectiveUI = null;

    [Header("ボス出現UI")]
    [SerializeField] private BossAppearUI bossAppearUI = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        countText.text = $"{bossSpwanCount - deathEnemyCount}体";

        UpdateObjective();
    }

    private void Update()
    {
        //BossSpwan();

        //ountText.text = $"{bossSpwanCount - deathEnemyCount}体";
    }

    /// <summary>
    /// 目標表示を更新処理
    /// </summary>
    private void UpdateObjective()
    {
        if (objectiveUI == null)
        {
            Debug.LogError("ObjectiveUI が設定されていません。");
            return;
        }

        //
        if (!isBossSpwan)
        {
            objectiveUI.SetObjective($"雑魚敵を倒せ！{deathEnemyCount}/{bossSpwanCount}");
        }
        else
        {
            objectiveUI.SetObjective("ボスを倒せ!");
        }
    }

    /// <summary>
    /// 敵撃破を加算
    /// </summary>
    public void AddDeathEnemyCount()
    {
        deathEnemyCount++;

        countText.text = $"{bossSpwanCount - deathEnemyCount}体";

        UpdateObjective();

        BossSpwan();
    }

    public void BossSpwan()
    {
        if (!isBossSpwan)
        {
            if (bossSpwanCount <= deathEnemyCount)
            {
                bossObject.SetActive(true);
                isBossSpwan = true;

                bossAppearUI.Show();

                UpdateObjective();
            }
        }
    }
}