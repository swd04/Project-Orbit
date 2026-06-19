using UnityEngine;

/// <summary>
/// ObjectiveUIテスト用
/// </summary>
public class ObjectiveUITest : MonoBehaviour
{
    [SerializeField] private ObjectiveUI objectiveUI = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            objectiveUI.SetObjective("てきをたおせ！");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            objectiveUI.SetObjective("Keyをさがせ！");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            objectiveUI.SetObjective("ボスをたおせ");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            objectiveUI.ClearObjective();
        }
    }
}