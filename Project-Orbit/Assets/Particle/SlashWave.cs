using UnityEngine;

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

    private void Update()
    {
        myRigidbody.linearVelocity = transform.rotation * moveVecter;

        SlashActive();
    }

    public void SlashActive()
    {
        spwanDelta += Time.deltaTime;

        if(activeTime <= spwanDelta)
        {
            gameObject.SetActive(false);
        }
    }
}
