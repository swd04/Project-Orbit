using UnityEngine;

public class UnitManager : SingletonMonoBehaviour<UnitManager>
{
    [SerializeField] private PhaseController phaseController = null;

    public void PhaseStart()
    {
        phaseController.PhaseStart();
    }
}
