using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TimeAttackSpwanEnemyTypes", menuName = "Scriptable Objects/TimeAttackSpwanEnemyTypes")]
public class TimeAttackSpwanEnemyTypes : ScriptableObject
{
    [Header("タイムアタックモードで出現させる敵の種類リスト")]
    [SerializeField] public List<EnemyType> enemyTypes;
}
