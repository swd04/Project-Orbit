using UnityEngine;
using DG.Tweening;
/// <summary>
/// プレイヤーが攻撃をする処理を行うクラス
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [Header("プレイヤーのTransform")]
    [SerializeField] private Transform playerTransform = null;

    [Header("武器のオブジェクト")]
    [SerializeField] private GameObject weaponObject = null;

    [Header("武器のコライダーの取得")]
    [SerializeField] private Collider weaponCollider = null;

    [Header("攻撃判定")]
    [SerializeField] private bool isAttack = false;

    [Header("マウスボタンを押した回数")]
    [SerializeField] public int attackCommandCount = 0;

    [Header("攻撃の回転の速さ")]
    [SerializeField] private float rotateSpeed = 0.0f;

    [Header("武器の回転角度")]
    [SerializeField] private Vector3 rotateAngle = Vector3.zero;



    private void Start()
    {
        isAttack = false;
        weaponCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();

            isAttack = true;
        }

        if (isAttack)
        {
            attackCommandCount++;
            weaponCollider.enabled = true;
        }

    }

    /// <summary>
    /// 攻撃処理を行うメソッド
    /// </summary>
    public void Attack()
    {
        weaponObject.transform.DOLocalRotate(new Vector3(rotateAngle.x, rotateAngle.y, rotateAngle.z), rotateSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            weaponObject.transform.DOLocalRotate(Vector3.zero, rotateSpeed).SetEase(Ease.Linear);

            isAttack = false;

            // コライダーを無効

            if (weaponCollider != null)
            {
                weaponCollider.enabled = false;
            }

        });
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 敵にダメージを与える処理をここに追加
            Debug.Log("敵に攻撃が当たった！");
        }
    }
}
