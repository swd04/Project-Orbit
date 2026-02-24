using UnityEngine;

public class PlayerChoseAttackMode : MonoBehaviour
{
    /// <summary>
    /// 武器のモードを選択するための列挙型
    /// </summary>
    public enum AttackMode
    {
        PREDATION,
        SOULREINFORCE
    }

    [Header("攻撃モードの選択")]
    [SerializeField] private AttackMode attackMode = AttackMode.SOULREINFORCE;

    [Header("現在の攻撃モード")]
    [SerializeField] public AttackMode currentAttackMode = AttackMode.SOULREINFORCE;

    public void SetAttackMode(AttackMode mode)
    {
        attackMode = mode;
        currentAttackMode = attackMode;
    }

    public AttackMode GetAttackMode()
    {
        return attackMode;
    }
}
