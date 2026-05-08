using UnityEngine;

/// <summary>
/// プレイヤーの強化するステータス
/// </summary>
[CreateAssetMenu(fileName = "PlayerStatusUp", menuName = "Scriptable Objects/PlayerStatusUp")]
public class PlayerStatusUp : ScriptableObject
{
    [Header("強化する体力の値")]
    [SerializeField] public int upLifePoint = 0;

    [Header("強化する攻撃力の値")]
    [SerializeField] public int upAttackPoint = 0;

    [Header("強化する防御力の値")]
    [SerializeField] public int upDefencePoint = 0;

    [Header("強化する移動速度の値")]
    [SerializeField] public float upMoveSpeed = 0.0f;

    [Header("強化するステータスのタイプ")]
    [SerializeField] public UpStatusType upStatusType = UpStatusType.None;

    public enum UpStatusType
    {
        None = -1,
        HpUp = 0,
        AttackPointUp = 1,
        DefencePointUp = 2,
        MoveSpeedUp = 3,
    }

    public void PlayerStatusEnchant()
    {
        switch (upStatusType)
        {
            case UpStatusType.None:break;
            case UpStatusType.HpUp:InGameManager.Instance.PlayerEnemyEating(upLifePoint,0,0,0.0f); break;
            case UpStatusType.AttackPointUp: InGameManager.Instance.PlayerEnemyEating(0, upAttackPoint, 0, 0.0f); break;
            case UpStatusType.DefencePointUp: InGameManager.Instance.PlayerEnemyEating(0, 0, upDefencePoint, 0.0f); break;
            case UpStatusType.MoveSpeedUp: InGameManager.Instance.PlayerEnemyEating(0, 0, 0, upMoveSpeed); break;
                default: break;
        }

    }
}
