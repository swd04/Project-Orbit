using UnityEngine;
using System.Collections.Generic;

public class SlashWave : MonoBehaviour
{
    [Header("リジッドボディ")]
    [SerializeField] private Rigidbody myRigidbody = null;

    [Header("移動力")]
    [SerializeField] private Vector3 moveVecter = Vector3.zero;

    [Header("有効な時間")]
    [SerializeField] private float activeTime = 0.0f;

    [Header("生成からの経過時間")]
    [SerializeField] private float spwanDelta = 0.0f;

    [Header("レベル別有効時間")]
    [SerializeField] private List<float> slashActiveLevel = new List<float>();

    [SerializeField] public int slashLevel = 0;

    private void Update()
    {
        myRigidbody.linearVelocity = transform.rotation * moveVecter;

        SlashActive();
    }

    public void SlashActive()
    {
        spwanDelta += Time.deltaTime;

        activeTime = slashActiveLevel[slashLevel];

        if (activeTime <= spwanDelta)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapObj"))
        {
            gameObject.SetActive(false);
        }
    }
}
