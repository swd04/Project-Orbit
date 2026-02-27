using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルホイールUI全体を制御する窓口クラス
/// </summary>
public class SkillWheelUI : MonoBehaviour
{
    [Header("スキルホイールの表示担当")]
    [SerializeField] private SkillWheelView view = null;

    [Header("スキルホイールのアニメーション担当")]
    [SerializeField] private SkillWheelAnimator animator = null;

    //スキルホイールの状態を保持するモデル
    private SkillWheelModel model = new SkillWheelModel();

    /// <summary>
    /// 現在選択中のスキルを取得する
    /// スキルが存在しない場合はnullを返す
    /// </summary>
    public SkillDate CurrentSkill =>
    model.Count > 0 ? model.Skills[model.CurrentIndex] : null;

    /// <summary>
    /// スキルホイールの初期化処理
    /// </summary>
    public void Initialize(List<SkillDate> skills, int startIndex)
    {
        //モデルを初期化
        model.Initialize(skills, startIndex);

        //現在のモデル状態を元にUIを構築
        view.Refresh(model);
    }

    /// <summary>
    /// スキルホイールを時計回りに回転させる処理
    /// </summary>
    public void RotateClockwise()
    {
        //共通回転処理に時計回りを指定
        Rotate(RotateDirection.Clockwise);
    }

    /// <summary>
    /// スキルホイールを反時計回りに回転させる処理
    /// </summary>
    public void RotateCounterClockwise()
    {
        //共通回転処理に反時計回りを指定
        Rotate(RotateDirection.CounterClockwise);
    }

    /// <summary>
    /// スキルホイール回転の共通処理
    /// </summary>
    private void Rotate(RotateDirection direction)
    {
        //アニメーション中、またはスキルが1つ以下の場合は処理しない
        if (animator.IsAnimating || model.Count <= 1) return;

        //回転方向に応じて選択中スキルのインデックスを更新
        model.Rotate(direction);

        //アニメーションを再生し、完了後に表示を更新する
        animator.PlayRotate(view, model, direction, () =>
        {
            //アニメーション後の最終状態をViewに反映
            view.Refresh(model);
        });
    }
}