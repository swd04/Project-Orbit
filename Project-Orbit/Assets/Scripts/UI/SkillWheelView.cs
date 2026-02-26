using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SkillWheelView : MonoBehaviour
{
    [Header("")]
    //
    [SerializeField] private SkillSlotUI slotCurrent;

    //
    [SerializeField] private SkillSlotUI slotNext;

    //
    [SerializeField] private SkillSlotUI slotPrev;

    [Header("")]
    //
    [SerializeField] private Transform posCurrent;

    //
    [SerializeField] private Transform posNext;

    //
    [SerializeField] private Transform posPrev;

    [Header("")]
    [SerializeField] private float moveDuration = 0.15f;

    //
    public bool IsAnimating { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public void Refresh(SkillWheelModel model)
    {
        //
        int count = model.Count;

        //
        slotCurrent.gameObject.SetActive(false);
        slotNext.gameObject.SetActive(false);
        slotPrev.gameObject.SetActive(false);

        //
        if (count <= 1) return;

        //
        slotCurrent.gameObject.SetActive(true);
        slotCurrent.transform.position = posCurrent.position;
        slotCurrent.Set(model.Skills[model.CurrentIndex], true);

        //
        slotNext.gameObject.SetActive(true);
        slotNext.transform.position = posNext.position;
        slotNext.Set(model.Skills[model.NextIndex], false);

        //
        if (count >= 3)
        {
            slotPrev.gameObject.SetActive(true);
            slotPrev.transform.position = posPrev.position;
            slotPrev.Set(model.Skills[model.PrevIndex], false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayRotate(SkillWheelModel model, RotateDirection direction)
    {
        //
        if (IsAnimating || model.Count <= 1) return;

        //
        StartCoroutine(RotateRoutine(model, direction));
    }

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator RotateRoutine(SkillWheelModel model, RotateDirection direction)
    {
        //
        IsAnimating = true;

        //
        if (model.Count == 2)
        {
            //
            yield return MovePair(
                slotCurrent.transform, posNext.position,
                slotNext.transform, posCurrent.position
            );
        }
        else
        {
            //
            if (direction == RotateDirection.CounterClockwise)
            {
                //
                yield return MoveTriple(
                    slotPrev.transform, posCurrent.position,
                    slotCurrent.transform, posNext.position,
                    slotNext.transform, posPrev.position
                );
            }
            else
            {
                //
                yield return MoveTriple(
                    slotPrev.transform, posNext.position,
                    slotCurrent.transform, posPrev.position,
                    slotNext.transform, posCurrent.position
                );
            }
        }

        //
        Refresh(model);

        //
        IsAnimating = false;
    }

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator MovePair(Transform a, Vector3 aTo, Transform b, Vector3 bTo)
    {
        //
        StartCoroutine(MoveTo(a, aTo));
        StartCoroutine(MoveTo(b, bTo));

        //
        yield return new WaitForSeconds(moveDuration);
    }

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator MoveTriple(Transform a, Vector3 aTo, Transform b, Vector3 bTo, Transform c, Vector3 cTo)
    {
        //
        StartCoroutine(MoveTo(a, aTo));
        StartCoroutine(MoveTo(b, bTo));
        StartCoroutine(MoveTo(c, cTo));

        //
        yield return new WaitForSeconds(moveDuration);
    }

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator MoveTo(Transform target, Vector3 to)
    {
        //
        Vector3 from = target.position;
        float t = 0f;

        //
        while (t < moveDuration)
        {
            t += Time.deltaTime;
            target.position = Vector3.Lerp(from, to, t / moveDuration);
            yield return null;
        }

        //
        target.position = to;
    }
}