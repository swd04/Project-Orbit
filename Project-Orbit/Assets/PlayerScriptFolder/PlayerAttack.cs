using DG.Tweening;
using UnityEngine;
using static PlayerChoseAttackMode;
/// <summary>
/// プレイヤーが攻撃をする処理を行うクラス
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [Header("PlayerChoseAttackModeを取得")]
    [SerializeField] private PlayerChoseAttackMode playerChoseAttackMode = null;

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

    [Header("モード切替のクールタイム")]
    [SerializeField] private float modeChangeCoolTime = 0.0f;

    [Header("攻撃の回転の速さ")]
    [SerializeField] private float rotateSpeed = 0.0f;

    [Header("武器の回転角度")]
    [SerializeField] private Vector3 rotateAngle = Vector3.zero;

    /// <summary>
    /// 初期化を行うメソッド
    /// </summary>
    private void Start()
    {
        isAttack = false;
        weaponCollider.enabled = false;
    }

    /// <summary>
    /// プレイヤーの攻撃処理を行うメソッド
    /// </summary>
    private void Update()
    {
        InputKey();

        // 攻撃コマンドの入力回数をカウントする処理
        if (isAttack)
        {
            attackCommandCount++;
            weaponCollider.enabled = true;
        }

    }

    /// <summary>
    /// キーボードを入力したときの処理全般を行うメソッド
    /// </summary>
    private void InputKey()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 左クリックで攻撃を行う処理

            Attack();

            isAttack = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //playerChoseAttackMode.GetAttackMode();

            // Spaceキーで攻撃モードを切り替える処理

            //攻撃モードをトグル切替
            playerChoseAttackMode.Toggle();

            //if (playerChoseAttackMode.currentAttackMode == AttackMode.PREDATION)
            //{
            //    // プレイヤーの攻撃モードを魂化に変更
            //    playerChoseAttackMode.SetAttackMode(AttackMode.SOULREINFORCE);
            //}
            //else if (playerChoseAttackMode.currentAttackMode == AttackMode.SOULREINFORCE)
            //{
            //    // プレイヤーの攻撃モードを捕食に変更
            //    playerChoseAttackMode.SetAttackMode(AttackMode.PREDATION);
            //}

            Debug.Log("現在の攻撃モードは" + playerChoseAttackMode.CurrentAttackMode + "です");
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

        // 攻撃コマンドの回数をカウント
        attackCommandCount++;
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