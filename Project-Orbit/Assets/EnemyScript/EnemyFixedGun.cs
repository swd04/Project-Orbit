using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 遠距離攻撃型の敵が
/// </summary>
public class EnemyFixedGun : MonoBehaviour
{
    [Header("EnemyAIControllerの取得")]
    [SerializeField] private EnemyAIController enemyAIController = null;


    [Header("弾のプレハブ")]
    [SerializeField] private GameObject weaponObject = null;

    [Header("攻撃動作を開始しているかの判定")]
    [SerializeField] private bool isAttacking = false;

    [Header("最初に生成しておくオブジェクトの数")]
    [SerializeField] private int initialSize = 0;

    [Header("これを攻撃として使用する敵のプレハブ")]
    [SerializeField] private Transform enemyPrefabTransform = null;

    private void Start()
    {
        if (enemyAIController == null)
        {
            enemyAIController = GetComponent<EnemyAIController>();
        }

        if (weaponObject == null)
        {
            Debug.LogError(weaponObject + "がnullです。");
        }


    }



    private void Update()
    {
        if (enemyAIController.isAttack && !isAttacking)
        {
            StartCoroutine(EnemyGun());
        }
    }

    private IEnumerator EnemyGun()
    {



        yield return null;
    }
}
