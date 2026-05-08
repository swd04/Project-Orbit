using UnityEngine;

public class SoulCore : MonoBehaviour
{
    public enum ActionType
    {
        None = -1,
        Type1 = 0,
        Type2 = 1,
        Type3 = 2,
        Type4 = 3,
        Type5 = 4,
    }

    [SerializeField] public ActionType actionType = ActionType.None;

    [SerializeField] private int actionAttackPoint = 0;

    [SerializeField] public int soulLevel = 0;

    public void SoulLevelUp()
    {
        soulLevel++;
    }

}
