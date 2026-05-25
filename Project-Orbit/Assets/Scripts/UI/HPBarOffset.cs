using UnityEngine;

/// <summary>
/// 
/// </summary>
public class HPBarOffset : MonoBehaviour
{
    [Header("")]
    [SerializeField] private float minOffset = 1.8f;

    [Header("")]
    [SerializeField] private float maxOffset = 2.2f;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        //
        Vector3 pos = transform.localPosition;

        //
        pos.y += Random.Range(minOffset, maxOffset);

        //
        transform.localPosition = pos;
    }
}