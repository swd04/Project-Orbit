using UnityEngine;

/// <summary>
/// BossHPBarUIテスト用
/// </summary>
public class BossHPBarUITest : MonoBehaviour
{
    [Header("ボスHPバーUI")]
    [SerializeField] private BossHPBarUI bossHPBarUI = null;

    private int currentHp;
    private int maxHp;

    private void Update()
    {
        //1キー：ボス出現
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            maxHp = 1000;
            currentHp = maxHp;

            bossHPBarUI.ShowBoss("Boss", maxHp);
        }

        //2キー：100ダメージ
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentHp -= 100;

            if (currentHp < 0)
            {
                currentHp = 0;
            }

            bossHPBarUI.UpdateBossHP(currentHp, maxHp);
        }

        //3キー：300ダメージ
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentHp -= 300;

            if (currentHp < 0)
            {
                currentHp = 0;
            }

            bossHPBarUI.UpdateBossHP(currentHp, maxHp);
        }

        //4キー：ボス撃破
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            bossHPBarUI.HideBoss();
        }
    }
}