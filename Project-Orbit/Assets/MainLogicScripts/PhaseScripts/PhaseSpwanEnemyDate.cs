using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PhaseSpwanEnemyDate", menuName = "Scriptable Objects/PhaseSpwanEnemyDate")]
public class PhaseSpwanEnemyDate : ScriptableObject
{
    [Header("出現させたい敵の種類リスト")]
    [SerializeField] public List<EnemyType> spwanEnemyTypes = new List<EnemyType>();

    [Header("出現させる敵の数(このリストの配列番号は敵の種類リストの配列番号と同じにすること)")]
    [SerializeField] public List<int> spwanEnemyCount = new List<int>();

    [Header("出現させる敵にステータス強化をつけるか否か(このリストの配列番号は敵の種類リストの配列番号と同じにすること)")]
    [SerializeField] public List<bool> spwanEnemyStatusBounus = new List<bool>();
}
