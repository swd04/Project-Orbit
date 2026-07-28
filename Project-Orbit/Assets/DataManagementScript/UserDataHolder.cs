using UnityEngine;

/// <summary>
/// 今回プレイしたユーザーのデータを保持するクラス
/// </summary>
public class UserDataHolder : MonoBehaviour
{
    [Header("プレイヤー名")]
    [SerializeField] public string userName = "";

    [Header("クリアタイム")]
    [SerializeField] public float clearTime = 0.0f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}