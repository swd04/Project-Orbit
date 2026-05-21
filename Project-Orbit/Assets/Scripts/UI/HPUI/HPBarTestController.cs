using UnityEngine;

/// <summary>
/// HPバー動作確認用テストスクリプト
/// 
/// Qキー：HPを10%減らす
/// Eキー：HPを10%増やす
/// </summary>
public class HPBarTestController : MonoBehaviour
{
    [Header("テスト設定")]

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
        //初期HP反映
        UpdateUI();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //HP減少
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeHP(-maxHP * changeRate);
        }

        //HP回復
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeHP(maxHP * changeRate);
        }
    }

    /// <summary>
    /// HP変更処理
    /// </summary>
    private void ChangeHP(float amount)
    {
        //HP変更
        currentHP = Mathf.Clamp(
            currentHP + amount,
            0f,
            maxHP
        );

        //UI更新
        UpdateUI();
    }

    /// <summary>
    /// UI更新処理
    /// </summary>
    private void UpdateUI()
    {
        //UIManager未生成時
        if (UIManager.Instance == null) return;

        //プレイヤーHP更新
        UIManager.Instance.UpdatePlayerHP(currentHP, maxHP);
    }
}