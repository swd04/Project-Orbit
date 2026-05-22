using UnityEngine;

/// <summary>
/// タグの保管クラス
/// </summary>
public class TagStock : SingletonBehaviour<TagStock>
{
    [Header("Weaponタグ")]
    [SerializeField] public string WEAPON_TAG = "Weapon";

    [Header("Playerタグ")]
    [SerializeField] public string PLAYER_TAG = "Player";

    [Header("Enemyタグ")]
    [SerializeField] public string ENEMY_TAG = "Enemy";

    [Header("EnemyWeaponタグ")]
    [SerializeField] public string ENEMY_WEAPON_TAG = "EnemyWeapon";

}
