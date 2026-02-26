using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SkillWheelUI : MonoBehaviour
{
    [Header("")]
    //
    [SerializeField] private SkillWheelView view = null;

    //
    private SkillWheelModel model = new SkillWheelModel();

    //
    public SkillDate CurrentSkill =>
    model.Count > 0 ? model.Skills[model.CurrentIndex] : null;

    /// <summary>
    /// 
    /// </summary>
    public void Initialize(List<SkillDate> skills, int startIndex)
    {
        //
        model.Initialize(skills, startIndex);
        view.Refresh(model);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RotateClockwise()
    {
        //
        Rotate(RotateDirection.Clockwise);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RotateCounterClockwise()
    {
        //
        Rotate(RotateDirection.CounterClockwise);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Rotate(RotateDirection direction)
    {
        //
        if (view.IsAnimating || model.Count <= 1) return;

        //
        model.Rotate(direction);
        view.PlayRotate(model, direction);
    }
}