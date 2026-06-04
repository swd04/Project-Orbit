using UnityEngine;

/// <summary>
/// ダメージ表示生成管理クラス
/// </summary>
public class DamageTextManager : MonoBehaviour
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    public static DamageTextManager Instance = null;

    [Header("ダメージ表示Prefab")]
    [SerializeField] private DamageTextUI damageTextPrefab = null;

    [Header("生成先Canvas")]
    [SerializeField] private Canvas canvas = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //シングルトン登録
        Instance = this;
    }

    /// <summary>
    /// ダメージ表示生成処理
    /// </summary>
    public void ShowDamage(int damage, Vector3 worldPosition)
    {
        //ワールド座標をスクリーン座標へ変換
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        //ダメージ表示生成
        DamageTextUI text =
            Instantiate(damageTextPrefab, screenPos,
            Quaternion.identity,
            canvas.transform);

        //ダメージ値設定
        text.SetDamage(damage);
    }
}