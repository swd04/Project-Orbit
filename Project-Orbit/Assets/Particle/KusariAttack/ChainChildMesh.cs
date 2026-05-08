using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class ChainChildMesh : MonoBehaviour
{
    [SerializeField] private Material originalChainMate = null;

    [SerializeField] private Material originalChainLightMate = null;

    [SerializeField] private List<MeshRenderer> chainMeshs = new List<MeshRenderer>();

    [SerializeField] private List<MeshRenderer> chainLightMeshs = new List<MeshRenderer>();

    [SerializeField] private float materialChangeTime = 0.0f;

    private void Start()
    {
        Material useChainMate = new Material(originalChainMate);

        Material useLightMate = new Material(originalChainLightMate);

        for(int i = 0; i < chainMeshs.Count; i++)
        {
            chainMeshs[i].material = useChainMate;
        }

        for(int i = 0;i < chainLightMeshs.Count; i++)
        {
            chainLightMeshs[i].material = useLightMate;
        }

        useChainMate.color = new Color32(0, 0, 0, 0);

        useLightMate.color = new Color32(0, 0, 0, 0);
    }


    public IEnumerator MeshActive()
    {
        yield return new WaitForSeconds(materialChangeTime);
    }
}
