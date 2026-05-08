using UnityEngine;

public class InGameManager : SingletonMonoBehaviour<InGameManager>
{
    [SerializeField] public PlayerStatus playerStatus = null;

    public void PlayerEnemyEating(int hp, int attack, int defence, float speed)
    {
        playerStatus.EnchantStatus(hp, attack, defence, speed);
    }
}
