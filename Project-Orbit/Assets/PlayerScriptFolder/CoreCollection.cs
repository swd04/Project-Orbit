using UnityEngine;

public class CoreCollection : MonoBehaviour
{
    [Header("コアの数")]
    [SerializeField] public int coreCount = 0;

    private void Start()
    {
        // コア取得数の初期化
        coreCount = 0;
    }

    /// <summary>
    /// コア取得のメソッド
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Core"))
        {
            // 獲得コアの数を増やす処理
            coreCount++;
        }
    }
}
