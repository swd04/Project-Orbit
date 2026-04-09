using UnityEngine;

/// <summary>
/// AIの行動採点の処理を行うクラス
/// </summary>
public class EnemyAIMovement : MonoBehaviour
{
    [Header("攻撃の持ち点")]
    [SerializeField] public int aiMovePointAttack = 0;

    [Header("プレイヤーから離れる際の持ち点")]
    [SerializeField] public int aiMovePointRemovePlayer = 0;

    [Header("待機状態の持ち点")]
    [SerializeField] public int aiMovePointWait = 0;

    [Header("逃げるの持ち点")]
    [SerializeField] public int aiMovePointEscape = 0;

    [Header("減点する数")]
    [SerializeField] private int aiMoveMinusPoint = 0;

    public EnemyStatus enemyStatus;


    public enum EnemyStatus
    {
        RemovePlayer,// 加点対象、プレイヤーとの距離が近いとき、隙を作るため
        Wait,// 加点対象、攻撃のインターバル
        Attack,// 減点対象、瀕死ではないときかつプレイヤーとの距離が近い
        Escape,// 加点対象、体力が低いかつプレイヤーとの距離が近いとき
    }

    
    private void Start()
    {

    }

    
    private void Update()
    {
        
        

        
        

        
    }

    public void EnemyAttack()
    {
        
    }
}
