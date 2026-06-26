using UnityEngine;

/// <summary>
/// 弾の当たり判定、その他付属したいものがあればここに書くクラス
/// </summary>
public class EnemyGunBullet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
