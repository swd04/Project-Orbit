using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// HPバー動作確認用テストスクリプト
/// 
/// Qキー：HPを10%減らす
/// Eキー：HPを10%増やす
/// </summary>
public class HPBarTestController : MonoBehaviour
{
    //最大HP
    [SerializeField] private float maxHP = 100f;

    //現在HP
    [SerializeField] private float currentHP = 100f;

    //HP増減率
    //0.1 = 10%
    [SerializeField] private float changeRate = 0.1f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        //ゲーム開始時にHPバーを初期状態へ反映
        UIManager.Instance.UpdatePlayerHP(currentHP, maxHP, false);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //指定のキーが押されたらHPを減少
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeHP(-maxHP * changeRate);
        }

        //指定のが押されたらHPを回復
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeHP(maxHP * changeRate);
        }
    }

    /// <summary>
    /// HPを増減させ、UIへ反映する処理
    /// </summary>
    private void ChangeHP(float amount)
    {
        //変更前のHPを保持
        float previousHP = currentHP;

        //HPを加算し、0～maxHPの範囲に制限
        currentHP = Mathf.Clamp(currentHP + amount, 0f, maxHP);

        //
        bool isDamaged = currentHP < previousHP;

        //
        UIManager.Instance.UpdatePlayerHP(currentHP, maxHP, isDamaged);
    }
}