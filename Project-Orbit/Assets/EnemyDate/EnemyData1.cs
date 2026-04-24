using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyData1", menuName = "Scriptable Objects/EnemyData1")]
public class EnemyData1 : ScriptableObject
{
    [Header("敵の最大HP")]
    [SerializeField] public int maxHp = 0;

    [Header("敵がプレイヤーを感知する距離")]
    [SerializeField] public float detectionRange = 0f;

    [Header("敵の移動速度")]
    [SerializeField] public float moveSpeed = 0f;

    [Header("どんな行動をするのか設定をする")]
    [SerializeField] public List<Enemy> actions = new List<Enemy>();
}
