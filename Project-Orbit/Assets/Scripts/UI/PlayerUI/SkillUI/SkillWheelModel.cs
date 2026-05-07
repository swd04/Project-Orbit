using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルホイールの「状態」だけを管理するモデルクラス
/// </summary>
[System.Serializable]
public class SkillWheelModel
{
    /// <summary>
    /// スキルホイールに登録されているスキル一覧
    /// </summary>
    public List<SkillDate> Skills { get; private set; } = new List<SkillDate>();

    /// <summary>
    /// 現在選択中のスキルのインデックス
    /// </summary>
    public int CurrentIndex { get; private set; }

    /// <summary>
    /// 登録されているスキル数
    /// </summary>
    public int Count => Skills?.Count ?? 0;

    /// <summary>
    /// 現在選択中スキルの「1つ前」のインデックス
    /// 先頭の場合は末尾にループする
    /// </summary>
    public int PrevIndex => Count <= 1 ? 0 : (CurrentIndex - 1 + Count) % Count;

    /// <summary>
    /// 現在選択中スキルの「1つ次」のインデックス
    /// 末尾の場合は先頭にループする
    /// </summary>
    public int NextIndex => Count <= 1 ? 0 : (CurrentIndex + 1) % Count;

    /// <summary>
    /// モデルの初期化処理
    /// </summary>
    public void Initialize(List<SkillDate> skills, int startIndex)
    {
        Skills = skills ?? new List<SkillDate>();
        CurrentIndex = Mathf.Clamp(startIndex, 0, Count - 1);
    }

    /// <summary>
    /// スキルホイールを回転させ、選択中インデックスを更新する処理
    /// </summary>
    public void Rotate(RotateDirection direction)
    {
        //スキルが1つ以下の場合は回転処理を行わない
        if (Count <= 1) return;

        //回転方向に応じて選択中インデックスを更新する
        if (direction == RotateDirection.Clockwise)
        {
            //時計回り：次のスキルを選択
            CurrentIndex = NextIndex;
        }
        else
        {
            //反時計回り：前のスキルを選択
            CurrentIndex = PrevIndex;
        }
    }
}