using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class ParticleRotate : MonoBehaviour
{
    [Header("回転角度")]
    [SerializeField] private Vector3 rotationVector = Vector3.zero;

    [Header("回転角度反転(x,y,z)")]
    [SerializeField] private List<bool> isReversFlag = new List<bool>();

    [Header("回転所要時間")]
    [SerializeField] private float rotationTime = 0.0f;

    [Header("子オブジェクトのTransformを回転させる場合にオン")]
    [SerializeField] private bool isLocalAxisMode = false;

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        if (!isLocalAxisMode)
        {
            transform.DORotate(rotationVector, rotationTime, RotateMode.WorldAxisAdd);

            if (isReversFlag[0])
            {
                rotationVector.x *= -1;
            }

            if (isReversFlag[1])
            {
                rotationVector.y *= -1;
            }

            if (isReversFlag[2])
            {
                rotationVector.z *= -1;
            }
        }
        else
        {
            transform.DORotate(rotationVector, rotationTime, RotateMode.LocalAxisAdd);

            if (isReversFlag[0])
            {
                rotationVector.x *= -1;
            }

            if (isReversFlag[1])
            {
                rotationVector.y *= -1;
            }

            if (isReversFlag[2])
            {
                rotationVector.z *= -1;
            }
        }
        
    }
}
