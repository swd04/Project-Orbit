using System.Collections;
using UnityEngine;

/// <summary>
/// スキルホイールのアニメーション制御を担当するクラス
/// </summary>
public class SkillWheelAnimator : MonoBehaviour
{
    [Header("スロット移動にかかる時間")]
    [SerializeField] private float moveDuration = 0.15f;

    /// <summary>
    /// 現在アニメーション再生中かどうか
    /// </summary>
    public bool IsAnimating { get; private set; }

    /// <summary>
    /// スキルホイール回転アニメーションを再生する処理
    /// </summary>
    public void PlayRotate(SkillWheelView view, SkillWheelModel model, RotateDirection direction, System.Action onComplete)
    {
        //再生中、または回転不要な場合は処理しない
        if (IsAnimating || model.Count <= 1) return;

        //回転アニメーション処理を開始
        StartCoroutine(RotateRoutine(view, model, direction, onComplete));
    }

    /// <summary>
    /// 回転アニメーションの処理
    /// </summary>
    private IEnumerator RotateRoutine(SkillWheelView view, SkillWheelModel model, RotateDirection direction, System.Action onComplete)
    {
        //アニメーション開始
        IsAnimating = true;

        //スキルが2つの場合
        if (model.Count == 2)
        {
            //Current⇔Nextを入れ替える
            yield return MovePair(
                view.CurrentSkillSlot, view.PosNext,
                view.NextSkillSlot, view.PosCurrent
            );
        }
        else
        {
            //スキルが3つ以上ある場合
            if (direction == RotateDirection.CounterClockwise)
            {
                //反時計回り回転
                yield return MoveTriple(
                    view.PreviousSkillSlot, view.PosCurrent,
                    view.CurrentSkillSlot, view.PosNext,
                    view.NextSkillSlot, view.PosPrev
                );
            }
            else
            {
                //時計回り回転
                yield return MoveTriple(
                    view.PreviousSkillSlot, view.PosNext,
                    view.CurrentSkillSlot, view.PosPrev,
                    view.NextSkillSlot, view.PosCurrent
                );
            }
        }

        //アニメーション終了
        IsAnimating = false;

        //完了通知
        onComplete?.Invoke();
    }

    /// <summary>
    /// 2スロット分の同時移動アニメーション処理
    /// </summary>
    private IEnumerator MovePair(Transform a, Vector3 aTo, Transform b, Vector3 bTo)
    {
        //各スロットを同時に移動開始
        StartCoroutine(MoveTo(a, aTo));
        StartCoroutine(MoveTo(b, bTo));

        //移動時間分待機
        yield return new WaitForSeconds(moveDuration);
    }

    /// <summary>
    /// 3スロット分の同時移動アニメーション処理
    /// </summary>
    private IEnumerator MoveTriple(Transform a, Vector3 aTo, Transform b, Vector3 bTo, Transform c, Vector3 cTo)
    {
        //各スロットを同時に移動開始
        StartCoroutine(MoveTo(a, aTo));
        StartCoroutine(MoveTo(b, bTo));
        StartCoroutine(MoveTo(c, cTo));

        //移動時間分待機
        yield return new WaitForSeconds(moveDuration);
    }

    /// <summary>
    /// 指定Transformを一定時間かけて移動させる処理
    /// </summary>
    private IEnumerator MoveTo(Transform target, Vector3 to)
    {
        //開始位置を記録
        Vector3 from = target.position;
        float t = 0f;

        //moveDuration秒かけて補間移動
        while (t < moveDuration)
        {
            t += Time.deltaTime;
            target.position = Vector3.Lerp(from, to, t / moveDuration);
            yield return null;
        }

        //最終位置を保証
        target.position = to;
    }
}