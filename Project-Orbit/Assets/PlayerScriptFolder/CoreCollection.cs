using UnityEditor;
using UnityEngine;
using static PlayerChoseAttackMode;

public class CoreCollection : MonoBehaviour
{
    [Header("コアの数")]
    [SerializeField] public int coreCount = 0;

    [Header("プレイヤーの武器モード")]
    [SerializeField] private PlayerChoseAttackMode playerChoseAttackMode = null;

    private void Start()
    {
        // コア取得数の初期化
        coreCount = 0;
    }

    private void Update()
    {

    }

    /// <summary>
    /// コア取得と取得した際にコア取得数を増やす処理
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (playerChoseAttackMode.currentAttackMode == AttackMode.SOULREINFORCE)
        {
            // コアに触れたとき、コア取得数を増やす
            if (other.gameObject.CompareTag("Core"))
            {
                coreCount++;
                Debug.Log("コアを取得しました 現在のコア数: " + coreCount);
            }
        }
    }
}
