using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// HPバー動作確認用テストスクリプト
/// 
/// Aキー：HPを10%減らす
/// Dキー：HPを10%増やす
/// </summary>
public class HPBarTestController : MonoBehaviour
{
    [SerializeField] private HPBarBase hpBarUI = null;
    [SerializeField] private DamageFlashUI damageFlashUI = null;

    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP = 100f;
    [SerializeField] private float changeRate = 0.1f;

    private void Start()
    {
        hpBarUI.UpdateHP(currentHP, maxHP);
    }

    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            ChangeHP(-maxHP * changeRate);
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            ChangeHP(maxHP * changeRate);
        }
    }

    private void ChangeHP(float amount)
    {
        float previousHP = currentHP;

        currentHP = Mathf.Clamp(currentHP + amount, 0f, maxHP);
        hpBarUI.UpdateHP(currentHP, maxHP);

        if (currentHP < previousHP)
        {
            damageFlashUI?.PlayDamageFlash();
        }
    }
}