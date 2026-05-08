using UnityEngine;

/// <summary>
/// 各敵のステータスの強化パラメーター
/// </summary>
[CreateAssetMenu(fileName = "EnemyStatusBounus", menuName = "Scriptable Objects/EnemyStatusBounus")]
public class EnemyStatusBounus : ScriptableObject
{
    [Header("HPの強化値")]
    [SerializeField] public int bounusLifePoint = 0;

    [Header("攻撃力の強化値")]
    [SerializeField] public int bounusAttackPoint = 0;

    [Header("防御力の強化値")]
    [SerializeField] public int bounusDefencePoint = 0;

    [Header("移動速度の強化値")]
    [SerializeField] public float bounusMoveSpeed = 0.0f;
}
