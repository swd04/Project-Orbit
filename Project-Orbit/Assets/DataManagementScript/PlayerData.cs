using UnityEngine;

public class PlayerData
{
    public string playerNameData = "";
    public float clearTimeData = 0.0f;

    public PlayerData(string name, float time)
    {
        playerNameData = name;
        clearTimeData = time;
    }
}
