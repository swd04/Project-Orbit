using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class SkillWheelModel
{
    //
    public List<SkillDate> Skills { get; private set; } = new List<SkillDate>();

    //
    public int CurrentIndex { get; private set; }

    //
    public int Count => Skills?.Count ?? 0;

    //
    public int PrevIndex => Count <= 1 ? 0 : (CurrentIndex - 1 + Count) % Count;

    //
    public int NextIndex => Count <= 1 ? 0 : (CurrentIndex + 1) % Count;

    /// <summary>
    /// 
    /// </summary>
    public void Initialize(List<SkillDate> skills, int startIndex)
    {
        //
        Skills = skills ?? new List<SkillDate>();
        CurrentIndex = Mathf.Clamp(startIndex, 0, Count - 1);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Rotate(RotateDirection direction)
    {
        //
        if (Count <= 1) return;

        //
        if (direction == RotateDirection.Clockwise)
        {
            CurrentIndex = NextIndex;
        }
        else
        {
            CurrentIndex = PrevIndex;
        }
    }
}