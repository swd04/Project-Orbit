using UnityEngine;

/// <summary>
/// ゲーム内の進行を管理するクラス
/// </summary>
public class TimeAttackManager : SingletonMonoBehaviour<TimeAttackManager>
{
    [Header("ゲームがプレイ中かどうか")]
    [SerializeField] public bool isPlayingGame = false;
}
