using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 遠距離攻撃型の敵が
/// </summary>
public class EnemyFixedGun : MonoBehaviour
{
    [Header("EnemyAIControllerの取得")]
    [SerializeField] private EnemyAIController enemyAIController = null;

    private EnemyGunObjectPool<EnemyGunBullet> bulletPool = null;

    [SerializeField] private List<EnemyGunBullet> bulletList = new List<EnemyGunBullet>();

    [Header("弾のプレハブ")]
    [SerializeField] private GameObject weaponObject = null;

    [Header("弾のrigidbody")]
    [SerializeField] private Rigidbody rigidbody = null;

    [Header("攻撃動作を開始しているかの判定")]
    [SerializeField] private bool isAttacking = false;

    [Header("最初に生成しておくオブジェクトの数")]
    [SerializeField] private int initialSize = 0;

    [Header("これを攻撃として使用する敵のプレハブ")]
    [SerializeField] private Transform enemyPrefabTransform = null;

    [Header("EnemyGunSDataを取得")]
    [SerializeField] private EnemyGunSData enemyGunData = null;

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

        bulletPool = new EnemyGunObjectPool<EnemyGunBullet>(weaponObject.GetComponent<EnemyGunBullet>(), initialSize);

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
        enemyAIController.agent.isStopped = true;
        isAttacking = true;

        EnemyGunBullet obj = bulletPool.Get();

        obj.transform.SetPositionAndRotation( transform.position, transform.rotation);

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(obj.transform.forward * enemyGunData.bulletPower, ForceMode.Impulse);

        bulletList.Add(obj);




        yield return new WaitForSeconds(2.0f);

       

        isAttacking = false;

        bulletPool.Release(bulletList[0]);
        bulletList.RemoveAt(0);

        yield return null;
    }
}
