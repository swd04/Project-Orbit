using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("生存時間")]
    [SerializeField] private float lifeTime = 1f;

    [Header("フェード速度")]
    [SerializeField] private float fadeSpeed = 2f;

    [Header("参照")]
    [SerializeField] private TextMeshProUGUI damageText;

    private Color originalColor;
    private float timer = 0.0f;

    private void Awake()
    {
        //同じオブジェクトにTMPを付ける設計前提
        damageText = GetComponentInChildren<TextMeshProUGUI>();

        if (damageText == null)
        {
            Debug.LogError("TMPが見つからない。Prefab構成を確認");
            return;
        }

        originalColor = damageText.color;
    }

    private void Update()
    {
        //上に浮く
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        //時間
        timer += Time.deltaTime;

        //フェード
        Color c = damageText.color;
        c.a -= fadeSpeed * Time.deltaTime;
        damageText.color = c;

        //消滅
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ダメージ設定
    /// </summary>
    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
        damageText.color = originalColor;
        timer = 0f;
    }
}