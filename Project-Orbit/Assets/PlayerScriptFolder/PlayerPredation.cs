using UnityEngine;

public class PlayerPredation : MonoBehaviour
{
    [Header("プレイヤーが捕食した数")]
    [SerializeField] public int predationedCount = 0;

    public void OnCollisionEnter(Collision collision)
    {
        // 仮でEnemyタグをあてはめておいてる
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 捕食した時の処理
            Debug.Log("捕食した");

            // 捕食した開かずを増やす処理
            predationedCount++;
        }
    }
}
