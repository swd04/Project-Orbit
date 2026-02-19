using UnityEngine;

/// <summary>
/// プレイヤーの管理を行うクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [Header("プレイヤーの取得")]
    [SerializeField] public GameObject playerObject = null;

    [Header("プレイヤーの位置を保持する変数")]
    [SerializeField] private Vector3 playerPosition = Vector3.zero;

    [Header("プレイヤー操作をまとめたスクリプト")]
    [SerializeField] private ControlPlayer playerController = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
