using System;
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

    //[Header("攻撃モードの選択")]
    //[SerializeField] private AttackMode attackMode = AttackMode.SOULREINFORCE;

    [Header("現在の攻撃モード")]
    [SerializeField] public AttackMode currentAttackMode = AttackMode.SOULREINFORCE;

    /// <summary>
    /// 攻撃モードが変更されたときに通知されるイベント
    /// UIや他システムはこれを購読して状態変化を検知する
    /// </summary>
    public event Action<AttackMode> OnAttackModeChanged;

    /// <summary>
    /// 現在の攻撃モードを取得する
    /// </summary>
    public AttackMode CurrentAttackMode => currentAttackMode;

    /// <summary>
    /// 武器のモードを設定するメソッド
    /// </summary>
    public void SetAttackMode(AttackMode mode)
    {
        //すでに同じモードなら処理しない
        if (currentAttackMode == mode) return;

        //攻撃モードを更新
        currentAttackMode = mode;

        //モード変更を購読者に通知
        OnAttackModeChanged?.Invoke(currentAttackMode);

        //attackMode = mode;
        //currentAttackMode = attackMode;
    }

    /// <summary>
    /// 攻撃モードを切り替える処理
    /// </summary>
    public void Toggle()
    {
        SetAttackMode(
        currentAttackMode == AttackMode.PREDATION
        ? AttackMode.SOULREINFORCE
        : AttackMode.PREDATION);
    }

    /// <summary>
    /// 武器のモードを取得するメソッド
    /// </summary>
    //public AttackMode GetAttackMode()
    //{
    //    return attackMode;
    //}
}